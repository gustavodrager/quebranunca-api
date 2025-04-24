using QNF.Plataforma.Application.Jogadores.Handlers;
using QNF.Plataforma.Application.Jogos.Handlers;
using QNF.Plataforma.Application.Rankings.Services;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJogadorRepository, JogadorRepository>();
builder.Services.AddScoped<CriarJogadorHandler>();
builder.Services.AddScoped<ValidarJogoHandler>();
builder.Services.AddScoped<RankingUpdater>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();