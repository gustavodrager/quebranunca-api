using QNF.Plataforma.Application.Commands.User;
using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Application.Handlers;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Core.ValueObjects;

public class RegisterUserCommandHandler 
    : CommandHandlerBase<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    protected override async Task<RegisterUserResponse> ExecuteAsync(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmailAsync(command.registerUserRequest.Email))
            throw new InvalidOperationException("Já existe um usuário com este e-mail.");

        var hashedPassword = _passwordHasher.Hash(command.registerUserRequest.Password);

        var email = new Email(command.registerUserRequest.Email);
        var password = new Password(hashedPassword);
        var user = new User(email, password);

        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();

        return new RegisterUserResponse
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email.Value,
            CreatedAt = user.DataCriacao
        };
    }

    protected override Task ValidateAsync(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.registerUserRequest.FullName))
            throw new ArgumentException("O nome completo é obrigatório.");

        if (string.IsNullOrWhiteSpace(command.registerUserRequest.Email))
            throw new ArgumentException("O e-mail é obrigatório.");

        if (string.IsNullOrWhiteSpace(command.registerUserRequest.Password) || command.registerUserRequest.Password.Length < 6)
            throw new ArgumentException("A senha deve ter pelo menos 6 caracteres.");

        return Task.CompletedTask;
    }
}
