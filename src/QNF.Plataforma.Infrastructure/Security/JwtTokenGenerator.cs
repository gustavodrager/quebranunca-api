using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Core.Entities;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using QNF.Plataforma.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using QNF.Plataforma.Application.Configurations;

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
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("jogadorId", user.JogadorId.ToString()) 
        };

        var token = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );

        var refreshToken = Guid.NewGuid().ToString();

        return new AuthResponse(new JwtSecurityTokenHandler().WriteToken(token), refreshToken);
    }
}
