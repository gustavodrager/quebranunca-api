using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Core.Interfaces;

public interface IDuplaRepository
{
    Task AdicionarAsync(Dupla dupla);
    Task<Dupla?> ObterPorIdAsync(Guid id);
    Task<bool> ExisteAsync(Guid jogador1Id, Guid jogador2Id);
    Task<IEnumerable<Dupla>> ObterPorGrupoAsync(Guid grupoId);
    Task<Dupla> ObterOuCriarAsync(Guid jogador1Id, Guid jogador2Id, Guid grupoId);
}