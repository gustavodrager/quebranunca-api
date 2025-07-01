using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.Infrastructure.Repositories;

public class GrupoRepository : IGrupoRepository
{
    private readonly PlataformaDbContext _context;

    public GrupoRepository(PlataformaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Grupo grupo)
    {
        _context.Grupos.Add(grupo);
        await _context.SaveChangesAsync();
    }

    public async Task<Grupo?> ObterPorIdAsync(Guid id)
    {
        return await _context.Grupos.FirstOrDefaultAsync(g => g.Id == id);
    }

        public async Task<Grupo?> ObterPorNomeAsync(string nome)
    {
        return await _context.Grupos.FirstOrDefaultAsync(g => g.Nome == nome);
    }

    public async Task<List<Grupo>> ListarAsync()
    {
        return await _context.Grupos.ToListAsync();
    }
}