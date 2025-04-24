using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Core.Interfaces;

public interface IGrupoRepository
{
    Task AdicionarAsync(Grupo grupo);
    Task<Grupo?> ObterPorIdAsync(Guid id);
    Task<List<Grupo>> ListarAsync();
}