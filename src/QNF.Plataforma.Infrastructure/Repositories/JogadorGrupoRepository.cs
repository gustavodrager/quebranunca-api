using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.Infrastructure.Repositories;

public class JogadorGrupoRepository : IJogadorGrupoRepository
{
    private readonly PlataformaDbContext _context;

    public JogadorGrupoRepository(PlataformaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(JogadorGrupo jogadorGrupo)
    {
        _context.JogadorGrupos.Add(jogadorGrupo);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExisteAsync(Guid jogadorId, Guid grupoId)
    {
        return await _context.JogadorGrupos
            .AnyAsync(jg => jg.JogadorId == jogadorId && jg.GrupoId == grupoId && jg.Ativo);
    }
}