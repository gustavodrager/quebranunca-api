using System;
using Microsoft.AspNetCore.Mvc;
using QNF.Plataforma.Application.Commands.Password;
using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Application.Interfaces;

namespace QNF.Plataforma.API.Controllers;

[ApiController]
[Route("api/users/password")]
public class PasswordController : ControllerBase
{
    private readonly ICommandHandler<SendPasswordResetTokenCommand, bool> _sendResetHandler;
    private readonly ICommandHandler<ResetPasswordCommand, bool> _resetHandler;

    public PasswordController(
        ICommandHandler<SendPasswordResetTokenCommand, bool> sendResetHandler,
        ICommandHandler<ResetPasswordCommand, bool> resetHandler)
    {
        _sendResetHandler = sendResetHandler;
        _resetHandler = resetHandler;
    }

    [HttpPost("reset-request")]
    public async Task<IActionResult> RequestPasswordReset(ForgotPasswordRequest request)
    {
        var command = new SendPasswordResetTokenCommand(request.Email);
        var success = await _sendResetHandler.HandleAsync(command);

        return success
            ? Ok(new { message = "Token enviado para o e-mail" })
            : NotFound(new { message = "Usuário não encontrado" });
    }

    [HttpPut]
    public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] ResetPasswordBodyRequest request)
    {
        var command = new ResetPasswordCommand(token, request.NewPassword);
        var success = await _resetHandler.HandleAsync(command);

        return success
            ? Ok(new { message = "Senha redefinida com sucesso" })
            : BadRequest(new { message = "Token inválido ou expirado" });
    }
}
