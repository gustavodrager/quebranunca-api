using Microsoft.AspNetCore.Mvc;
using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Application.Interfaces;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest request) =>
        Ok(await _authService.RefreshTokenAsync(request.RefreshToken));

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        try
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex) when (ex.Message == "User already exists")
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var success = await _authService.SendPasswordResetTokenAsync(request.Email);
        return success ? Ok(new { message = "Token enviado para o e-mail" }) 
                    : NotFound(new { message = "Usuário não encontrado" });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var success = await _authService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
        return success ? Ok(new { message = "Senha redefinida com sucesso" })
                    : BadRequest(new { message = "Token inválido ou expirado" });
    }

}