using QNF.Plataforma.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

public class PasswordHasherAdapter : IPasswordHasher
{
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string plainTextPassword)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword))
            throw new ArgumentException("A senha não pode ser nula ou vazia.");

        return _hasher.HashPassword(null, plainTextPassword);
    }

    public bool Verify(string hashedPassword, string plainTextPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword) || string.IsNullOrWhiteSpace(plainTextPassword))
            return false;

        var result = _hasher.VerifyHashedPassword(null, hashedPassword, plainTextPassword);
        return result == PasswordVerificationResult.Success;
    }
}
