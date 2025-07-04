using System;
using MediatR;
using QNF.Plataforma.Application.DTOs;

namespace QNF.Plataforma.Application.Commands.Auth;

public record RefreshTokenCommand(string RefreshToken) : IRequest<LoginResponse>;
