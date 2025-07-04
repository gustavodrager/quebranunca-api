namespace QNF.Plataforma.Application.DTOs;

public class AuthResponse
{
    /// <summary>
    /// Token JWT de acesso.
    /// </summary>
    public string AccessToken { get; }

    /// <summary>
    /// Data/hora de expiração do Access Token (UTC).
    /// </summary>
    public DateTime AccessTokenExpiresAt { get; }

    /// <summary>
    /// Token de refresh para renovação do JWT.
    /// </summary>
    public string RefreshToken { get; }

    /// <summary>
    /// Data/hora de expiração do Refresh Token (UTC).
    /// </summary>
    public DateTime RefreshTokenExpiresAt { get; }

    public AuthResponse(string accessToken, DateTime accessTokenExpiresAt, string refreshToken, DateTime refreshTokenExpiresAt)
    {
        AccessToken = accessToken;
        AccessTokenExpiresAt = accessTokenExpiresAt;
        RefreshToken = refreshToken;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;
    }
}
