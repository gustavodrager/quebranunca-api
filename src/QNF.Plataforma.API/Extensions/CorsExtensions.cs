using System;

namespace QNF.Plataforma.API.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var frontOrigin = configuration.GetValue<string>("FrontEndOrigin") ?? "http://localhost:5173";

        services.AddCors(options =>
        {
            options.AddPolicy("FrontCorsPolicy", policy =>
            {
                policy.WithOrigins(frontOrigin)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
            });
        });

        return services;
    }
}
