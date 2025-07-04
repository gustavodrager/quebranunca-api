using MediatR;
using QNF.Plataforma.Application.Commands.Auth;
using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Core.Interfaces;

namespace QNF.Plataforma.Application.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        // 1️⃣ Buscar usuário pelo e-mail
        var user = await _userRepository.GetByEmailAsync(command.Email)
            ?? throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

        // 2️⃣ Verificar a senha
        if (!_passwordHasher.Verify(user.Password.HashedValue, command.Password))
            throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

        // 3️⃣ Gerar tokens (JWT + Refresh)
        var authTokens = _jwtTokenGenerator.GenerateTokens(user);

        // 4️⃣ Atualizar Refresh Token no banco
        user.SetRefreshToken(authTokens.RefreshToken, authTokens.RefreshTokenExpiresAt);
        await _userRepository.UpdateAsync(user);

        // 5️⃣ Commit da transação
        await _unitOfWork.CommitAsync();

        // 6️⃣ Retornar resposta
        return new LoginResponse
        {
            UserId = user.Id,
            AccessToken = authTokens.AccessToken,
            AccessTokenExpiresAt = authTokens.AccessTokenExpiresAt,
            RefreshToken = authTokens.RefreshToken,
            RefreshTokenExpiresAt = authTokens.RefreshTokenExpiresAt
        };
    }
}
