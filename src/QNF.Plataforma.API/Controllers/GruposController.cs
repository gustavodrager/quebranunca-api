using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QNF.Plataforma.Application.Grupos.Commands;
using QNF.Plataforma.Application.Grupos.Handlers;

namespace QNF.Plataforma.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class GruposController : ControllerBase
{
    private readonly CriarGrupoHandler _handler;
    private readonly AdicionarJogadorAoGrupoHandler _adicionarJogadorHandler;

    public GruposController(CriarGrupoHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarGrupoCommand command)
    {
        var id = await _handler.Handle(command);
        return CreatedAtAction(nameof(Criar), new { id }, new { id });
    }
    
    [HttpPost("{grupoId}/adicionar-jogador/{jogadorId}")]
    public async Task<IActionResult> AdicionarJogadorAoGrupo(Guid grupoId, Guid jogadorId)
    {
        var command = new AdicionarJogadorAoGrupoCommand(grupoId, jogadorId);
        await _adicionarJogadorHandler.Handle(command);
        return NoContent();
    }
}