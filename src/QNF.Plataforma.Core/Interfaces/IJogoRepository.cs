using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Core.Interfaces;

public interface IJogoRepository
{
    Task AdicionarAsync(Jogo jogo);
    Task<Jogo?> ObterPorIdAsync(Guid id);
    Task<List<Jogo>> ListarPorGrupoAsync(Guid grupoId);
    Task<Jogo?> ObterUltimoAsync();
    Task<Jogo?> ObterUltimoAprovadoAsync();
}