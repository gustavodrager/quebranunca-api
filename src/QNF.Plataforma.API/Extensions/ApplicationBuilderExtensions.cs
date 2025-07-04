using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void InitializeDatabases(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var writeContext = serviceProvider.GetRequiredService<WriteDbContext>();
        writeContext.Database.EnsureCreated();

        var readContext = serviceProvider.GetRequiredService<ReadDbContext>();
        readContext.Database.EnsureCreated();
    }
}