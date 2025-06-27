using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Core.Interfaces;

public interface IRankingRepository
{
    Task<Ranking?> ObterAsync(Guid grupoId, Guid jogadorId);
    Task AdicionarAsync(Ranking ranking);
    Task AtualizarAsync(Ranking ranking);
    Task<List<Ranking>> ListarPorGrupoAsync(Guid grupoId);
    Task<List<Ranking>> ListarRankingGeralAsync();
}