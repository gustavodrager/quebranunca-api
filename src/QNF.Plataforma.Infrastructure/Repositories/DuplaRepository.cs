using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.Infrastructure.Repositories;

public class DuplaRepository : IDuplaRepository
{
    private readonly PlataformaDbContext _context;

    public DuplaRepository(PlataformaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Dupla dupla)
    {
        _context.Duplas.Add(dupla);
        await _context.SaveChangesAsync();
    }

    public async Task<Dupla?> ObterPorIdAsync(Guid id)
    {
        return await _context.Duplas.FindAsync(id);
    }

    public async Task<IEnumerable<Dupla>> ObterPorGrupoAsync(Guid grupoId)
    {
        return await _context.Duplas
            .Where(d => d.Id == grupoId)
            .ToListAsync();
    }

    public async Task<bool> ExisteAsync(Guid jogador1Id, Guid jogador2Id)
    {
        return await _context.Duplas.AnyAsync(d =>
            (d.Jogador1Id == jogador1Id && d.Jogador2Id == jogador2Id) ||
            (d.Jogador1Id == jogador2Id && d.Jogador2Id == jogador1Id));
    }

    public async Task<Dupla> ObterOuCriarAsync(Guid jogador1Id, Guid jogador2Id, Guid grupoId)
    {
        var duplaExistente = await _context.Duplas
            .FirstOrDefaultAsync(d =>
                d.Id == grupoId &&
                ((d.Jogador1Id == jogador1Id && d.Jogador2Id == jogador2Id) ||
                (d.Jogador1Id == jogador2Id && d.Jogador2Id == jogador1Id)));

        if (duplaExistente != null)
            return duplaExistente;

        var novaDupla = new Dupla(jogador1Id, jogador2Id);
        await _context.Duplas.AddAsync(novaDupla);
        await _context.SaveChangesAsync();

        return novaDupla;
    }
}
