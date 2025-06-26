using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Application.Jogos.Handlers;
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
using QNF.Plataforma.API.Configurations;
using QNF.Plataforma.Infrastructure.Services;
using QNF.Plataforma.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddDbContext<PlataformaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IJogadorRepository, JogadorRepository>();
builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();
builder.Services.AddScoped<IDuplaRepository, DuplaRepository>();
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<IValidacaoJogoRepository, ValidacaoJogoRepository>();
builder.Services.AddScoped<IRankingRepository, RankingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<CriarJogadorHandler>();
builder.Services.AddScoped<CriarGrupoHandler>();
builder.Services.AddScoped<AdicionarJogadorAoGrupoHandler>();
builder.Services.AddScoped<CriarDuplaHandler>();
builder.Services.AddScoped<RegistrarJogoHandler>();
builder.Services.AddScoped<ValidarJogoHandler>();
builder.Services.AddScoped<RankingUpdater>();

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sua-chave-secreta")),
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "QNF Plataforma API v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();