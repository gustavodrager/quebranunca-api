using System;
using MassTransit;
using Microsoft.Extensions.Options;
using QNF.Plataforma.API.Configurations;
using QNF.Plataforma.Infrastructure.Consumers;

namespace QNF.Plataforma.API.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));

        services.AddMassTransit(x =>
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

        return services;
    }
}