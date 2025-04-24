using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;

namespace QNF.Plataforma.Application.Rankings.Services;

public class RankingUpdater
{
    private readonly IRankingRepository _rankingRepository;
    private readonly IDuplaRepository _duplaRepository;

    public RankingUpdater(
        IRankingRepository rankingRepository,
        IDuplaRepository duplaRepository)
    {
        _rankingRepository = rankingRepository;
        _duplaRepository = duplaRepository;
    }

    public async Task AtualizarAsync(Jogo jogo)
    {
        if (jogo.Status != JogoStatus.Aprovado)
            return;

        var dupla1 = await _duplaRepository.ObterPorIdAsync(jogo.Dupla1Id);
        var dupla2 = await _duplaRepository.ObterPorIdAsync(jogo.Dupla2Id);

        if (dupla1 == null || dupla2 == null) return;

        bool dupla1Venceu = jogo.PontuacaoDupla1 > jogo.PontuacaoDupla2;

        await AtualizarJogadores(jogo.GrupoId, dupla1.Jogador1Id, dupla1Venceu);
        await AtualizarJogadores(jogo.GrupoId, dupla1.Jogador2Id, dupla1Venceu);
        await AtualizarJogadores(jogo.GrupoId, dupla2.Jogador1Id, !dupla1Venceu);
        await AtualizarJogadores(jogo.GrupoId, dupla2.Jogador2Id, !dupla1Venceu);
    }

    private async Task AtualizarJogadores(Guid grupoId, Guid jogadorId, bool venceu)
    {
        var ranking = await _rankingRepository.ObterAsync(grupoId, jogadorId);

        if (ranking is null)
        {
            ranking = new Ranking(grupoId, jogadorId);
            if (venceu) ranking.RegistrarVitoria();
            else ranking.RegistrarDerrota();

            await _rankingRepository.AdicionarAsync(ranking);
        }
        else
        {
            if (venceu) ranking.RegistrarVitoria();
            else ranking.RegistrarDerrota();

            await _rankingRepository.AtualizarAsync(ranking);
        }
    }
}