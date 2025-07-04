namespace QNF.Plataforma.Core.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime RefreshTokenExpiry { get; private set; }

    public User(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
    }

    public void SetRefreshToken(string refreshToken, DateTime expiry)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiry = expiry;
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }
}