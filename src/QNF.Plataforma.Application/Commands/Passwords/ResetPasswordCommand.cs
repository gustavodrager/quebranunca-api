using System;
using MediatR;

namespace QNF.Plataforma.Application.Commands.Password;

public record ResetPasswordCommand(string Token, string NewPassword) : IRequest<bool>;