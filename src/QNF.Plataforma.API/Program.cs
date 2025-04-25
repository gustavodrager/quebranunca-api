using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Application.Jogos.Handlers;
using QNF.Plataforma.Application.Rankings.Services;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data;
using QNF.Plataforma.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PlataformaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJogadorRepository, JogadorRepository>();
builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();
builder.Services.AddScoped<IDuplaRepository, DuplaRepository>();
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<IValidacaoJogoRepository, ValidacaoJogoRepository>();
builder.Services.AddScoped<IRankingRepository, RankingRepository>();

builder.Services.AddScoped<RegistrarJogoHandler>();
builder.Services.AddScoped<ValidarJogoHandler>();
builder.Services.AddScoped<RankingUpdater>();

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