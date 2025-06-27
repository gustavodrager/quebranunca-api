using QNF.Plataforma.Application.DTOs;

namespace QNF.Plataforma.Application.Interfaces;

public interface IJogoService
{
    Task<UltimoJogoResponse?> ObterUltimoAprovadoAsync();
}
