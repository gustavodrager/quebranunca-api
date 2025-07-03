using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands;
using QNF.Application.DTOs;
using QNF.Plataforma.Application.Queries;

namespace QNF.Plataforma.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IMediator _mediator;
    public GamesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateGameCommand cmd)
    {
        var id = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetGameByIdQuery(id);
        var game = await _mediator.Send(query);
        return game is not null ? Ok(game) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetAll()
    {
        var games = await _mediator.Send(new GetAllGamesQuery());
        return Ok(games);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateGameCommand cmd)
    {
        if (id != cmd.Id) return BadRequest();
        await _mediator.Send(cmd);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteGameCommand(id));
        return NoContent();
    }
}