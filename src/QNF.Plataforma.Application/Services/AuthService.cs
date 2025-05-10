using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;

namespace QNF.Plataforma.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmailSender _emailSender;

    public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IEmailSender emailSender)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _emailSender = emailSender;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User already exists");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User(request.Email, passwordHash);

        await _userRepository.AddAsync(user);

        var tokens = _jwtTokenGenerator.GenerateTokens(user);
        user.SetRefreshToken(tokens.RefreshToken, DateTime.UtcNow.AddDays(7));

        await _userRepository.UpdateAsync(user);

        return tokens;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        var tokens = _jwtTokenGenerator.GenerateTokens(user);
        user.SetRefreshToken(tokens.RefreshToken, DateTime.UtcNow.AddDays(7));

        await _userRepository.UpdateAsync(user);

        return tokens;
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);
        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Invalid or expired refresh token");

        var tokens = _jwtTokenGenerator.GenerateTokens(user);
        user.SetRefreshToken(tokens.RefreshToken, DateTime.UtcNow.AddDays(7));

        await _userRepository.UpdateAsync(user);

        return tokens;
    }

    public async Task<bool> SendPasswordResetTokenAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return false;

        var token = Guid.NewGuid().ToString();
        user.SetRefreshToken(token, DateTime.UtcNow.AddHours(1));
        await _userRepository.UpdateAsync(user);

        await _emailSender.SendEmailAsync(user.Email, "Token de Redefinição de Senha", $"Seu token é: {token}");

        return true;
    }

    public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null || user.RefreshToken != token || user.RefreshTokenExpiry < DateTime.UtcNow)
            return false;

        user.UpdatePassword(BCrypt.Net.BCrypt.HashPassword(newPassword));
        user.SetRefreshToken(string.Empty, DateTime.MinValue); // Invalida o token

        await _userRepository.UpdateAsync(user);

        return true;
    }
}