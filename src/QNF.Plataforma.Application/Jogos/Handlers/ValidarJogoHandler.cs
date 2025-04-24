using QNF.Plataforma.Application.Jogos.Commands;
using QNF.Plataforma.Application.Rankings.Services;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;

namespace QNF.Plataforma.Application.Jogos.Handlers;

public class ValidarJogoHandler
{
    private readonly IJogoRepository _jogoRepository;
    private readonly IValidacaoJogoRepository _validacaoRepository;
    private readonly IDuplaRepository _duplaRepository;
    private readonly RankingUpdater _rankingUpdater;

    public ValidarJogoHandler(
        IJogoRepository jogoRepository,
        IValidacaoJogoRepository validacaoRepository,
        IDuplaRepository duplaRepository,
        RankingUpdater rankingUpdater)
    {
        _jogoRepository = jogoRepository;
        _validacaoRepository = validacaoRepository;
        _duplaRepository = duplaRepository;
        _rankingUpdater = rankingUpdater;
    }

    public async Task Handle(ValidarJogoCommand command)
    {
        var jogo = await _jogoRepository.ObterPorIdAsync(command.JogoId);
        if (jogo == null)
            throw new Exception("Jogo não encontrado.");

        var jaValidou = await _validacaoRepository.JaValidouAsync(command.JogoId, command.JogadorId);
        if (jaValidou)
            throw new Exception("Jogador já validou este jogo.");

        var validacao = new ValidacaoJogo(command.JogoId, command.JogadorId, command.Status, command.Comentario);
        await _validacaoRepository.AdicionarAsync(validacao);

        // Obtem todas validações feitas para o jogo
        var validacoes = await _validacaoRepository.ObterPorJogoAsync(command.JogoId);

        var dupla1 = await _duplaRepository.ObterPorIdAsync(jogo.Dupla1Id);
        var dupla2 = await _duplaRepository.ObterPorIdAsync(jogo.Dupla2Id);

        bool dupla1Validou = validacoes.Any(v => v.JogadorId == dupla1?.Jogador1Id || v.JogadorId == dupla1?.Jogador2Id);
        bool dupla2Validou = validacoes.Any(v => v.JogadorId == dupla2?.Jogador1Id || v.JogadorId == dupla2?.Jogador2Id);
        bool algumaReprovacao = validacoes.Any(v => v.Status == ValidacaoStatus.Reprovado);

        if (algumaReprovacao)
        {
            jogo.Rejeitar();
        }
        else if (dupla1Validou && dupla2Validou)
        {
            jogo.Aprovar();
            await _rankingUpdater.AtualizarAsync(jogo);
        }

        await _jogoRepository.AdicionarAsync(jogo); 
    }
}
