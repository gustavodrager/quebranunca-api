using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QNF.Plataforma.API.Requests;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Application.Jogos.Commands;
using QNF.Plataforma.Application.Jogos.Handlers;
using QNF.Plataforma.Application.DTOs;

namespace QNF.Plataforma.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class JogosController : ControllerBase
{
    private readonly RegistrarJogoHandler _handler;
    private readonly ValidarJogoHandler _validarHandler;
    private readonly IJogoService _jogoService;

    public JogosController(RegistrarJogoHandler handler, ValidarJogoHandler validarHandler, IJogoService jogoService)
    {
        _handler = handler;
        _validarHandler = validarHandler;
        _jogoService = jogoService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarJogo([FromBody] CriarJogoRequest request)
    {
        var jogadorIdClaim = User.FindFirst("jogadorId")?.Value;
        if (jogadorIdClaim is null)
            return Unauthorized("ID do jogador não encontrado no token.");

        var criadoPorJogadorId = Guid.Parse(jogadorIdClaim);

        try
        {
            var jogoId = await _jogoService.CriarJogoAsync(request, criadoPorJogadorId);
            return CreatedAtAction(nameof(ObterPorId), new { id = jogoId }, new { id = jogoId });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var jogo = await _jogoService.ObterPorIdAsync(id);
        return jogo is not null ? Ok(jogo) : NotFound();
    }
}