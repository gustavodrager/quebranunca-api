using Microsoft.AspNetCore.Mvc;
using QNF.Plataforma.Application.Jogadores.Commands;
using QNF.Plataforma.Application.Jogadores.Handlers;

namespace QNF.Plataforma.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JogadoresController : ControllerBase
{
    private readonly CriarJogadorHandler _handler;

    public JogadoresController(CriarJogadorHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> CriarJogador([FromBody] CriarJogadorCommand command)
    {
        var id = await _handler.Handle(command);
        return CreatedAtAction(nameof(CriarJogador), new { id }, new { id });
    }
}