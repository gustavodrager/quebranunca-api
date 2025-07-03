using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Enums;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.Infrastructure.Repositories;

public class JogoRepository : IJogoRepository
{
    private readonly PlataformaDbContext _context;

    public JogoRepository(PlataformaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Jogo jogo)
    {
        var tracking = await _context.Jogos.FindAsync(jogo.Id);
        if (tracking == null)
            _context.Jogos.Add(jogo); // novo
        else
            _context.Jogos.Update(jogo); // atualização

        await _context.SaveChangesAsync();
    }

    public async Task<Jogo?> ObterPorIdAsync(Guid id)
    {
        return await _context.Jogos
            .AsNoTracking()
            .FirstOrDefaultAsync(j => j.Id == id);
    }

    public async Task<List<Jogo>> ListarPorGrupoAsync(Guid grupoId)
    {
        return await _context.Jogos
            .Where(j => j.GrupoId == grupoId)
            .OrderByDescending(j => j.DataHora)
            .ToListAsync();
    }

    public async Task<Jogo?> ObterUltimoAprovadoAsync()
    {
        return await _context.Jogos
            .Where(j => j.Status == JogoStatus.Aprovado)
            .OrderByDescending(j => j.DataHora)
            .FirstOrDefaultAsync();
    }

    public async Task<Jogo?> ObterUltimoAsync()
    {
        return await _context.Jogos
            .Where(j => j.Status == JogoStatus.Aprovado)
            .OrderByDescending(j => j.DataHora)
            .FirstOrDefaultAsync();
    }
}