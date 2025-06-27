using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Application.Interfaces;

public interface IJogadorService
{
    Task<Guid> CriarJogadorAsync(CriarJogadorRequest request);
    Task<List<Jogador>> ObterTodosAsync();
    Task<Jogador?> ObterPorIdAsync(Guid id);
    Task AtualizarNomeAsync(Guid jogadorId, string? nome);
}