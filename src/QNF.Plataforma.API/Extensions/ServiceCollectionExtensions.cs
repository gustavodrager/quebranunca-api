using System;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data;
using QNF.Plataforma.Infrastructure.Repositories;
using QNF.Plataforma.Infrastructure.Security;
using QNF.Plataforma.Infrastructure.Services;

namespace QNF.Plataforma.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IEmailSender, EmailSender>();
        return services;
    }
}
