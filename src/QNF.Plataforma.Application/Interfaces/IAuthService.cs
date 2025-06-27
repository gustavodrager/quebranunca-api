using QNF.Plataforma.Application.DTOs;

namespace QNF.Plataforma.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RefreshTokenAsync(string refreshToken);
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<bool> SendPasswordResetTokenAsync(string email);
    Task<bool> ResetPasswordAsync(string token, string newPassword);
}
