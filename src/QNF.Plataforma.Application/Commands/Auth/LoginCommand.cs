using System;
using MediatR;
using QNF.Plataforma.Application.DTOs;

namespace QNF.Plataforma.Application.Commands.Auth;

public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;

