using System;
using MediatR;
using QNF.Plataforma.Application.Commands.Auth;
using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Application.Interfaces;

namespace QNF.Plataforma.Application.Handlers;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var authResponse = await _authService.RefreshTokenAsync(command.RefreshToken);
        return new LoginResponse
        {
            AccessToken = authResponse.AccessToken,
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(15),
            RefreshToken = authResponse.RefreshToken,
            RefreshTokenExpiresAt = DateTime.Today.AddHours(1) 
        };
    }
}