using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Core.Interfaces;

public interface IJwtTokenGenerator
{
    AuthResponse GenerateTokens(User user);
}
