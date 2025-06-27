using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Application.Interfaces;

namespace QNF.Plataforma.Application.Services;

public class JogoService : IJogoService
{
    private readonly IJogoRepository _jogoRepository;
    private readonly IRankingService _rankingService;

    public JogoService(IJogoRepository jogoRepository, IRankingService rankingService)
    {
        _jogoRepository = jogoRepository;
        _rankingService = rankingService;
    }

    public async Task AdicionarAsync(Jogo jogo)
    {
        await _jogoRepository.AdicionarAsync(jogo);
        await _rankingService.AtualizarRankingPorJogoAsync(jogo);
    }

    public async Task<Jogo?> ObterPorIdAsync(Guid id)
    {
        return await _jogoRepository.ObterPorIdAsync(id);
    }

    public async Task<List<Jogo>> ListarPorGrupoAsync(Guid grupoId)
    {
        return await _jogoRepository.ListarPorGrupoAsync(grupoId);
    }

    public async Task<UltimoJogoResponse?> ObterUltimoAprovadoAsync()
    {
        var jogo = await _jogoRepository.ObterUltimoAprovadoAsync();
        if (jogo == null) return null;

        return new UltimoJogoResponse
        {
            Dupla1Id = jogo.Dupla1Id,
            Dupla2Id = jogo.Dupla2Id,
            PontuacaoDupla1 = jogo.PontuacaoDupla1,
            PontuacaoDupla2 = jogo.PontuacaoDupla2,
            DataHora = jogo.DataHora
        };
    }
}