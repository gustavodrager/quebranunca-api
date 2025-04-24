using QNF.Plataforma.Application.Duplas.Commands;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;

namespace QNF.Plataforma.Application.Duplas.Handlers;

public class CriarDuplaHandler
{
    private readonly IDuplaRepository _duplaRepository;
    private readonly IJogadorRepository _jogadorRepository;

    public CriarDuplaHandler(IDuplaRepository duplaRepository, IJogadorRepository jogadorRepository)
    {
        _duplaRepository = duplaRepository;
        _jogadorRepository = jogadorRepository;
    }

    public async Task<Guid> Handle(CriarDuplaCommand command)
    {
        // Verificar se os jogadores existem
        var jogador1 = await _jogadorRepository.ObterPorIdAsync(command.Jogador1Id);
        var jogador2 = await _jogadorRepository.ObterPorIdAsync(command.Jogador2Id);

        if (jogador1 is null || jogador2 is null)
            throw new Exception("Jogador inválido.");

        // Verificar se dupla já existe
        var jaExiste = await _duplaRepository.ExisteAsync(command.Jogador1Id, command.Jogador2Id);
        if (jaExiste)
            throw new Exception("Dupla já existe.");

        var dupla = new Dupla(command.Jogador1Id, command.Jogador2Id);
        await _duplaRepository.AdicionarAsync(dupla);

        return dupla.Id;
    }
}