namespace QNF.Plataforma.Application.DTOs;

public class LoginResponse
{
    public Guid UserId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiresAt { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiresAt { get; set; }
}
