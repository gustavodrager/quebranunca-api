using QNF.Plataforma.Application.Jogos.Commands;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;

namespace QNF.Plataforma.Application.Jogos.Handlers;

public class RegistrarJogoHandler
{
    private readonly IJogoRepository _jogoRepository;
    private readonly IGrupoRepository _grupoRepository;
    private readonly IDuplaRepository _duplaRepository;

    public RegistrarJogoHandler(
        IJogoRepository jogoRepository,
        IGrupoRepository grupoRepository,
        IDuplaRepository duplaRepository)
    {
        _jogoRepository = jogoRepository;
        _grupoRepository = grupoRepository;
        _duplaRepository = duplaRepository;
    }

    public async Task<Guid> Handle(RegistrarJogoCommand command)
    {
        // Validações básicas (grupo e duplas existentes)
        var grupo = await _grupoRepository.ObterPorIdAsync(command.GrupoId);
        if (grupo is null)
            throw new Exception("Grupo inválido.");

        var dupla1 = await _duplaRepository.ObterPorIdAsync(command.Dupla1Id);
        var dupla2 = await _duplaRepository.ObterPorIdAsync(command.Dupla2Id);

        if (dupla1 is null || dupla2 is null)
            throw new Exception("Uma ou ambas as duplas são inválidas.");

        var jogo = new Jogo(
            command.GrupoId,
            command.Dupla1Id,
            command.Dupla2Id,
            command.CriadoPorJogadorId,
            command.DataHora,
            command.Local
        );

        jogo.RegistrarResultado(command.PontuacaoDupla1, command.PontuacaoDupla2);

        await _jogoRepository.AdicionarAsync(jogo);

        return jogo.Id;
    }
}