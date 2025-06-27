using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;

namespace QNF.Plataforma.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJogadorRepository _jogadorRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmailSender _emailSender;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IEmailSender emailSender,
        IJogadorRepository jogadorRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _emailSender = emailSender;
        _jogadorRepository = jogadorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User already exists");

        var jogador = new Jogador
        {
            Email = request.Email
        };

        await _jogadorRepository.AdicionarAsync(jogador);
        await _unitOfWork.CommitAsync();

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User(request.Email, passwordHash);

        user.AtribuirJogador(jogador.Id); 

        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();

        var tokens = _jwtTokenGenerator.GenerateTokens(user);
        user.SetRefreshToken(tokens.RefreshToken, DateTime.UtcNow.AddDays(7));

        await _userRepository.UpdateAsync(user);
        await _unitOfWork.CommitAsync();

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
        await _unitOfWork.CommitAsync();

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
        await _unitOfWork.CommitAsync();

        return tokens;
    }

    public async Task<bool> SendPasswordResetTokenAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return false;

        var token = Guid.NewGuid().ToString();
        user.SetRefreshToken(token, DateTime.UtcNow.AddHours(1));
        await _userRepository.UpdateAsync(user);
        await _unitOfWork.CommitAsync();

        var clientUrl = "http://localhost:5173";
        var resetLink = $"{clientUrl}/reset-password?token={token}";

        var emailBody = $@"
            <html>
                <body style='font-family: Arial, sans-serif; color: #333;'>
                    <h2>Recuperação de Senha - QuebraNunca</h2>
                    <p>Você solicitou a redefinição da sua senha.</p>
                    <p>Clique no botão abaixo para criar uma nova senha:</p>
                    <p>
                        <a href='{resetLink}' 
                        style='display:inline-block;padding:10px 20px;background-color:#facc15;color:#000;text-decoration:none;border-radius:5px;'>
                            Redefinir Senha
                        </a>
                    </p>
                    <p>Se você não solicitou isso, ignore este e-mail.</p>
                </body>
            </html>";

        await _emailSender.SendEmailAsync(user.Email, "Recuperação de Senha - QuebraNunca", emailBody);

        return true;
    }

    public async Task<bool> ResetPasswordAsync(string token, string newPassword)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(token);
        if (user == null || user.RefreshToken != token || user.RefreshTokenExpiry < DateTime.UtcNow)
            return false;

        user.UpdatePassword(BCrypt.Net.BCrypt.HashPassword(newPassword));
        user.SetRefreshToken(string.Empty, DateTime.MinValue);

        await _userRepository.UpdateAsync(user);
        await _unitOfWork.CommitAsync();

        return true;
    }
}