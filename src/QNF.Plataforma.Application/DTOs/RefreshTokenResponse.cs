using System;

namespace QNF.Plataforma.Application.DTOs;

public class RefreshTokenResponse
{
    public string AccessToken { get; set; }
    public DateTime ExpiresAt { get; set; }
}