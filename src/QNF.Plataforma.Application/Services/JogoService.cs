using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Application.DTOs;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Core.Enums;

namespace QNF.Plataforma.Application.Services;

public class JogoService : IJogoService
{
    private readonly IJogadorRepository _jogadorRepository;    
    private readonly IGrupoRepository _grupoRepository;
    private readonly IJogoRepository _jogoRepository;
    private readonly IRankingService _rankingService;
    private readonly IDuplaRepository _duplaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public JogoService(
        IJogadorRepository jogadorRepository,
        IGrupoRepository grupoRepository,
        IJogoRepository jogoRepository,
        IRankingService rankingService,
        IDuplaRepository duplaRepository,
        IUnitOfWork unitOfWork)
    {
        _jogadorRepository = jogadorRepository;
        _grupoRepository = grupoRepository;
        _jogoRepository = jogoRepository;
        _rankingService = rankingService;
        _duplaRepository = duplaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CriarJogoAsync(CriarJogoRequest request, Guid jogadorLogadoId)
    {
        var jogadores = new Jogador[4];
        var nomes = request.TimeA.Concat(request.TimeB).ToArray();

        for (int i = 0; i < nomes.Length; i++)
        {
            var nome = nomes[i].Trim();

            var jogador = await _jogadorRepository.ObterPorNomeAsync(nome);
            if (jogador == null)
            {
                jogador = new Jogador
                {
                    Nome = nome,
                    Status = StatusEntidade.Pendente
                };

                await _jogadorRepository.AdicionarAsync(jogador);
            }

            jogadores[i] = jogador;
        }

        var grupo = await _grupoRepository.ObterPorNomeAsync(request.GrupoNome);
        if (grupo == null)
        {
            grupo = new Grupo(request.GrupoNome, jogadorLogadoId);
            await _grupoRepository.AdicionarAsync(grupo);
        }
        
        var dupla1 = await _duplaRepository.ObterOuCriarAsync(jogadores[0].Id , jogadores[1].Id, grupo.Id);
        var dupla2 = await _duplaRepository.ObterOuCriarAsync(jogadores[2].Id, jogadores[3].Id, grupo.Id);

        var jogo = new Jogo(grupo.Id, dupla1.Id, dupla2.Id, jogadorLogadoId, DateTime.UtcNow, null);

        jogo.RegistrarResultado(request.PlacarA, request.PlacarB);

        await _jogoRepository.AdicionarAsync(jogo);

        return jogo.Id;
    }

    public async Task AdicionarAsync(Jogo jogo)
    {
        await _jogoRepository.AdicionarAsync(jogo);
        await _rankingService.AtualizarRankingPorJogoAsync(jogo);
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

    public async Task<object?> ObterPorIdAsync(Guid id)
    {
        return await _jogoRepository.ObterPorIdAsync(id);
    }
}