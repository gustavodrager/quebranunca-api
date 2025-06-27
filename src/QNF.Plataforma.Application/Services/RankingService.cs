using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;

public class RankingService : IRankingService
{
    private readonly IRankingRepository _rankingRepo;
    private readonly IJogadorRepository _jogadorRepo;
    private readonly IDuplaRepository _duplaRepository;

    public RankingService(IRankingRepository rankingRepo, IJogadorRepository jogadorRepo, IDuplaRepository duplaRepository)
    {
        _rankingRepo = rankingRepo;
        _jogadorRepo = jogadorRepo;
        _duplaRepository = duplaRepository;
    }

    public async Task<List<RankingResponse>> ObterRankingPorGrupoAsync(Guid grupoId)
    {
        var rankings = await _rankingRepo.ListarPorGrupoAsync(grupoId);
        var jogadores = await _jogadorRepo.ListarAsync();

        var resultado = rankings
            .Join(jogadores,
                r => r.JogadorId,
                j => j.Id,
                (r, j) => new RankingResponse
                {
                    NomeJogador = j.Nome,
                    Jogos = r.Jogos,
                    Vitorias = r.Vitorias,
                    Derrotas = r.Derrotas,
                    Aproveitamento = r.Aproveitamento
                })
            .OrderByDescending(r => r.Vitorias)
            .ToList();

        return resultado;
    }

    public async Task AtualizarRankingPorJogoAsync(Jogo jogo)
    {
        // Dupla vencedora e perdedora
        bool dupla1Venceu = jogo.PontuacaoDupla1 > jogo.PontuacaoDupla2;

        var dupla1 = await _duplaRepository.ObterPorIdAsync(jogo.Dupla1Id);
        var dupla2 = await _duplaRepository.ObterPorIdAsync(jogo.Dupla2Id);

        if (dupla1 == null || dupla2 == null)
            throw new Exception("Uma das duplas não foi encontrada.");

        var jogadoresVencedores = dupla1Venceu ? dupla1.Jogadores : dupla2.Jogadores;
        var jogadoresPerdedores = dupla1Venceu ? dupla2.Jogadores : dupla1.Jogadores;

        // Atualiza ranking de vencedores
        foreach (var jogador in jogadoresVencedores)
        {
            var ranking = await _rankingRepo.ObterAsync(jogo.GrupoId, jogador.Id);
            if (ranking == null)
            {
                ranking = new Ranking(jogo.GrupoId, jogador.Id);
                ranking.RegistrarVitoria();
                await _rankingRepo.AdicionarAsync(ranking);
            }
            else
            {
                ranking.RegistrarVitoria();
                await _rankingRepo.AtualizarAsync(ranking);
            }
        }

        foreach (var jogador in jogadoresPerdedores)
        {
            var ranking = await _rankingRepo.ObterAsync(jogo.GrupoId, jogador.Id);
            if (ranking == null)
            {
                ranking = new Ranking(jogo.GrupoId, jogador.Id);
                ranking.RegistrarDerrota();
                await _rankingRepo.AdicionarAsync(ranking);
            }
            else
            {
                ranking.RegistrarDerrota();
                await _rankingRepo.AtualizarAsync(ranking);
            }
        }
    }
}
