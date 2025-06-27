using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QNF.Plataforma.API.Requests;
using QNF.Plataforma.Application.Jogos.Commands;
using QNF.Plataforma.Application.Jogos.Handlers;

namespace QNF.Plataforma.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class JogosController : ControllerBase
{
    private readonly RegistrarJogoHandler _handler;
    private readonly ValidarJogoHandler _validarHandler;

    public JogosController(RegistrarJogoHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Registrar([FromBody] RegistrarJogoCommand command)
    {
        var id = await _handler.Handle(command);
        return CreatedAtAction(nameof(Registrar), new { id }, new { id });
    }
    
    [HttpPost("{jogoId}/validacoes")]
    public async Task<IActionResult> ValidarJogo(Guid jogoId, [FromBody] ValidarJogoRequest request)
    {
        var command = new ValidarJogoCommand(
            jogoId,
            request.JogadorId,
            request.Status,
            request.Comentario
        );

        await _validarHandler.Handle(command);
        return NoContent();
    }
}
