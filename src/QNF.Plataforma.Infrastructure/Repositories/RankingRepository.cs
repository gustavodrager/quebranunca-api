using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.Infrastructure.Repositories;

public class RankingRepository : IRankingRepository
{
    private readonly PlataformaDbContext _context;

    public RankingRepository(PlataformaDbContext context)
    {
        _context = context;
    }

    public async Task<Ranking?> ObterAsync(Guid grupoId, Guid jogadorId)
    {
        return await _context.Rankings
            .FirstOrDefaultAsync(r => r.GrupoId == grupoId && r.JogadorId == jogadorId);
    }

    public async Task AdicionarAsync(Ranking ranking)
    {
        _context.Rankings.Add(ranking);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Ranking ranking)
    {
        _context.Rankings.Update(ranking);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Ranking>> ListarPorGrupoAsync(Guid grupoId)
    {
        return await _context.Rankings
            .Where(r => r.GrupoId == grupoId)
            .ToListAsync();
    }
}