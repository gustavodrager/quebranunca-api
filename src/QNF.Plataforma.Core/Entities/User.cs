using QNF.Plataforma.Core.ValueObjects;

namespace QNF.Plataforma.Core.Entities;

public class User : BaseEntity
{
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public string? FullName { get; set; }
    public string? RefreshToken { get; private set; }
    public DateTime RefreshTokenExpiry { get; private set; }

    protected User() { }

    public User(Email email, Password password)
    {
        Email = email;
        Password = password;
    }

    public void SetRefreshToken(string refreshToken, DateTime expiry)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiry = expiry;
    }

    public void UpdatePassword(Password newPassword)
    {
        Password = newPassword;
    }
}
