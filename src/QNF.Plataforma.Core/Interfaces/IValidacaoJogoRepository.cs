using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Core.Interfaces;

public interface IValidacaoJogoRepository
{
    Task AdicionarAsync(ValidacaoJogo validacao);
    Task<List<ValidacaoJogo>> ObterPorJogoAsync(Guid jogoId);
    Task<bool> JaValidouAsync(Guid jogoId, Guid jogadorId);
}