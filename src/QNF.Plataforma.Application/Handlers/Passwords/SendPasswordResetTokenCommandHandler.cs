using System;
using MediatR;
using QNF.Plataforma.Application.Commands.Password;

namespace QNF.Plataforma.Application.Handlers;

public class SendPasswordResetTokenCommandHandler : IRequestHandler<SendPasswordResetTokenCommand, bool>
{
    public async Task<bool> Handle(SendPasswordResetTokenCommand command, CancellationToken cancellationToken)
    {
        return true; // Placeholder for actual token sending logic
    }
}
