using QNF.Plataforma.Application.DTOs;

namespace QNF.Plataforma.Application.Interfaces;

public interface IJogoService
{
    Task<UltimoJogoResponse?> ObterUltimoAprovadoAsync();
    Task AdicionarAsync(Core.Entities.Jogo jogo);
    Task<Guid> CriarJogoAsync(CriarJogoRequest request, Guid criadoPorJogadorId);
    Task<object?> ObterPorIdAsync(Guid id);
}
