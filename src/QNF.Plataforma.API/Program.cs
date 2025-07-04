using QNF.Plataforma.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCorsPolicy(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddDatabaseContexts(builder.Configuration)
    .AddSwaggerDocumentation()
    .AddApplicationServices()
    .AddMassTransitRabbitMq(builder.Configuration)
    .AddRedisCache(builder.Configuration);

var app = builder.Build();

app.UseSwaggerDocumentation();
app.UseCors("FrontCorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.InitializeDatabases();

app.Run();
