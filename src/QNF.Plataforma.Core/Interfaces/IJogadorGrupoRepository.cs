using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Core.Interfaces;

public interface IJogadorGrupoRepository
{
    Task AdicionarAsync(JogadorGrupo jogadorGrupo);
    Task<bool> ExisteAsync(Guid jogadorId, Guid grupoId);
}