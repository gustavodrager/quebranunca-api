using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.Infrastructure.Repositories;

public class JogadorRepository : IJogadorRepository
{
    private readonly PlataformaDbContext _context;

    public JogadorRepository(PlataformaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Jogador jogador)
    {
        _context.Jogadores.Add(jogador);
        await _context.SaveChangesAsync();
    }

    public async Task<Jogador?> ObterPorIdAsync(Guid id)
    {
        return await _context.Jogadores.FindAsync(id);
    }

    public async Task<List<Jogador>> ListarAsync()
    {
        return await _context.Jogadores.ToListAsync();
    }
}