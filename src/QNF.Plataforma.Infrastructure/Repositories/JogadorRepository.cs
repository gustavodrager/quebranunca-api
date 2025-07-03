using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Enums;
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

    public async Task<List<Jogador>> BuscarPorPrefixoAsync(string prefixo)
    {
        return await _context.Jogadores
            .Where(j => j.Nome != null && j.Nome.StartsWith(prefixo, StringComparison.OrdinalIgnoreCase))
            .Where(j => j.Status != StatusEntidade.Inativo)
            .OrderBy(j => j.Nome)
            .ToListAsync();
    }

    public Task<Jogador> BuscarPorNomeExatoAsync(string prefixo)
    {
        throw new NotImplementedException();
    }

    public Task<List<Jogador>> ObterPorIdsAsync(IEnumerable<Guid> ids)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Jogador?> ObterPorNomeAsync(string nome)
    {
        return await _context.Jogadores
            .FirstOrDefaultAsync(j => j.Nome.ToLower() == nome.ToLower());
    }

    public async Task<Jogador> ObterOuCriarPorNomeAsync(string nome)
    {
        var jogador = await ObterPorNomeAsync(nome);
        if (jogador != null) return jogador;

        var novo = new Jogador {Id = Guid.NewGuid(), Nome = nome, Status = StatusEntidade.Pendente };
        _context.Jogadores.Add(novo);
        await _context.SaveChangesAsync();
        return novo;
    }
}