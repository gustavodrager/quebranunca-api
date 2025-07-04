using System;
using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.API.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseContexts(this IServiceCollection services, IConfiguration configuration)
    {
        // WriteDbContext
        services.AddDbContext<WriteDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("WriteConnection")));

        // ReadDbContext
        services.AddDbContext<ReadDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("ReadConnection")));

        return services;
    }
}
