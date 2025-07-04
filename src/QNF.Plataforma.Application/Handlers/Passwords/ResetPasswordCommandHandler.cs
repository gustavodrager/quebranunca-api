using System;
using MediatR;
using QNF.Plataforma.Application.Commands.Password;

namespace QNF.Plataforma.Application.Handlers;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
{

    public async Task<bool> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        return true; // Placeholder for actual password reset logic);
    }
}
