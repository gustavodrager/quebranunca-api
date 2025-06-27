using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Core.Entities;

public interface IRankingService
{
    Task<List<RankingResponse>> ObterRankingPorGrupoAsync(Guid grupoId);
    Task AtualizarRankingPorJogoAsync(Jogo jogo);
}
