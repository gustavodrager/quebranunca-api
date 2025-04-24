using QNF.Plataforma.Application.Jogadores.Commands;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;

namespace QNF.Plataforma.Application.Jogadores.Handlers;

public class CriarJogadorHandler
{
    private readonly IJogadorRepository _repository;

    public CriarJogadorHandler(IJogadorRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CriarJogadorCommand command)
    {
        var jogador = new Jogador(command.Nome, command.Apelido, command.Telefone, command.Email);
        await _repository.AdicionarAsync(jogador);
        return jogador.Id;
    }
}