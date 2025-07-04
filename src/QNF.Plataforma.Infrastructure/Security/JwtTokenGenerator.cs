using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Core.Entities;
using System.Security.Claims;
using System.Text;
using QNF.Plataforma.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using QNF.Plataforma.Application.Configurations;
using System.Security.Cryptography;

namespace QNF.Plataforma.Infrastructure.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtConfiguration _configuration;

    public JwtTokenGenerator(IOptions<JwtConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public AuthResponse GenerateTokens(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpiryMinutes);
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(_configuration.RefreshTokenExpiryDays);

        var token = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            claims: claims,
            expires: accessTokenExpiresAt,
            signingCredentials: creds
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = GenerateSecureRefreshToken();

        return new AuthResponse(
            accessToken,
            accessTokenExpiresAt,
            refreshToken,
            refreshTokenExpiresAt
        );
    }
    private string GenerateSecureRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
