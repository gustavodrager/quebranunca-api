using QNF.Plataforma.Application.Grupos.Commands;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;

namespace QNF.Plataforma.Application.Grupos.Handlers;

public class CriarGrupoHandler
{
    private readonly IGrupoRepository _repository;

    public CriarGrupoHandler(IGrupoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CriarGrupoCommand command)
    {
        var grupo = new Grupo(command.Nome, command.CriadoPorJogadorId, command.Descricao);
        await _repository.AdicionarAsync(grupo);
        return grupo.Id;
    }
}