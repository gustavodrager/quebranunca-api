using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Core.Interfaces;

public interface IJogadorRepository
{
    Task AdicionarAsync(Jogador jogador);
    Task<Jogador?> ObterPorIdAsync(Guid id);
    Task<List<Jogador>> ListarAsync();
}