using System;
using MediatR;

namespace QNF.Plataforma.Application.Commands.Password;

public record SendPasswordResetTokenCommand(string Email) : IRequest<bool>;
