using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Application.Jogos.Handlers;
using QNF.Plataforma.Application.Grupos.Handlers;
using QNF.Plataforma.Application.Duplas.Handlers;
using QNF.Plataforma.Application.Rankings.Services;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data;
using QNF.Plataforma.Infrastructure.Repositories;
using QNF.Plataforma.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Application.Services;
using QNF.Plataforma.Application.Configurations;
using QNF.Plataforma.Infrastructure.Services;
using StackExchange.Redis;
using System.Reflection;
using MassTransit;
using QNF.Plataforma.Infrastructure.Consumers;
using QNF.Plataforma.API.Configurations;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var frontOrigin = "http://localhost:5173";

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontCorsPolicy",
        policy =>
        {
            policy.WithOrigins(frontOrigin)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddDbContext<PlataformaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
      b => b.MigrationsAssembly("QNF.Plataforma.Infrastructure")));

builder.Services.AddDbContext<WriteDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WriteConnection")));
    
builder.Services.AddDbContext<ReadDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ReadConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "QNF Plataforma API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer.\n\nDigite: Bearer {seu token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(QNF.Plataforma.Application.Handlers.GetAllGamesHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJogadorService, JogadorService>();
builder.Services.AddScoped<IJogoService, JogoService>();
builder.Services.AddScoped<IRankingService, RankingService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJogadorRepository, JogadorRepository>();
builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();
builder.Services.AddScoped<IJogadorGrupoRepository, JogadorGrupoRepository>();
builder.Services.AddScoped<IDuplaRepository, DuplaRepository>();
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<IValidacaoJogoRepository, ValidacaoJogoRepository>();
builder.Services.AddScoped<IRankingRepository, RankingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJogadorGrupoRepository, JogadorGrupoRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

builder.Services.AddScoped<CriarGrupoHandler>();
builder.Services.AddScoped<AdicionarJogadorAoGrupoHandler>();
builder.Services.AddScoped<CriarDuplaHandler>();
builder.Services.AddScoped<RegistrarJogoHandler>();
builder.Services.AddScoped<ValidarJogoHandler>();
builder.Services.AddScoped<RankingUpdater>();

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtConfiguration>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "QNF.Plataforma",
            ValidAudience = "QNF.Plataforma",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));

builder.Services.AddMassTransit( x =>
{
    x.AddConsumer<GameCreatedConsumer>();
    
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbit = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value;

        cfg.Host(rabbit.Host, rabbit.VirtualHost, h =>
        {
            h.Username(rabbit.Username);
            h.Password(rabbit.Password);
        });

        cfg.ReceiveEndpoint("game-created-queue", e =>
        {
            e.ConfigureConsumer<GameCreatedConsumer>(context);
        });
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "QNF Plataforma API v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors("FrontCorsPolicy");

using (var scope = app.Services.CreateScope())
{
    var writeCtx = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
    writeCtx.Database.EnsureCreated();

    var readCtx = scope.ServiceProvider.GetRequiredService<ReadDbContext>();
    readCtx.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();