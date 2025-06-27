using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QNF.Plataforma.Application.Duplas.Commands;
using QNF.Plataforma.Application.Duplas.Handlers;

namespace QNF.Plataforma.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class DuplasController : ControllerBase
{
    private readonly CriarDuplaHandler _handler;

    public DuplasController(CriarDuplaHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> CriarDupla([FromBody] CriarDuplaCommand command)
    {
        var id = await _handler.Handle(command);
        return CreatedAtAction(nameof(CriarDupla), new { id }, new { id });
    }
}
