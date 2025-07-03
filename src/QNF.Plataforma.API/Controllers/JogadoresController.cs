using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QNF.Plataforma.API.Requests;
using QNF.Plataforma.Application.Interfaces;

namespace QNF.Plataforma.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class JogadoresController : ControllerBase
{
    private readonly IJogadorService _service;

    public JogadoresController(IJogadorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CriarJogador([FromBody] CriarJogadorRequest request)
    {
        var id = await _service.CriarJogadorAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id }, new { id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var jogador = await _service.ObterPorIdAsync(id);
        return jogador != null ? Ok(jogador) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var jogadores = await _service.ObterTodosAsync();
        return Ok(jogadores);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetPerfil()
    {
        var jogadorId = User.FindFirst("jogadorId")?.Value;
        if (jogadorId == null) return Unauthorized();

        var jogador = await _service.ObterPorIdAsync(Guid.Parse(jogadorId));
        if (jogador == null) return NotFound();

        return Ok(new
        {
            jogador.Id,
            jogador.Nome,
            jogador.Email
        });
    }

    [HttpPatch("me")]
    public async Task<IActionResult> AtualizarPerfil([FromBody] AtualizarPerfilRequest request)
    {
        var jogadorId = User.FindFirst("jogadorId")?.Value;
        if (jogadorId == null) return Unauthorized();

        await _service.AtualizarNomeAsync(Guid.Parse(jogadorId), request.Nome);

        return NoContent();
    }
    
    [HttpGet("buscar")]
    public async Task<IActionResult> BuscarPorPrefixo([FromQuery] string prefixo)
    {
        if (string.IsNullOrWhiteSpace(prefixo) || prefixo.Length < 3)
            return BadRequest("Informe pelo menos 3 letras para buscar.");

        var jogadores = await _service.BuscarPorPrefixoAsync(prefixo);
        return Ok(jogadores);
    }
}