using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QNF.Plataforma.Application.Interfaces;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RankingController : ControllerBase
{
    private readonly IRankingService _service;

    public RankingController(IRankingService service)
    {
        _service = service;
    }

    [HttpGet("{grupoId}")]
    public async Task<IActionResult> ObterRanking(Guid grupoId)
    {
        var ranking = await _service.ObterRankingPorGrupoAsync(grupoId);
        return Ok(ranking);
    }
}
