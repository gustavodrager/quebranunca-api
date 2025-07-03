using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Core.Interfaces;

public interface IJogadorRepository
{
    Task AdicionarAsync(Jogador jogador);
    Task<Jogador?> ObterPorIdAsync(Guid id);
    Task<List<Jogador>> ObterPorIdsAsync(IEnumerable<Guid> ids);
    Task<List<Jogador>> ListarAsync();
    Task<List<Jogador>> BuscarPorPrefixoAsync(string prefixo);
    Task<Jogador> BuscarPorNomeExatoAsync(string prefixo);
    Task<Jogador?> ObterPorNomeAsync(string nome);
    Task<Jogador> ObterOuCriarPorNomeAsync(string nome);
}