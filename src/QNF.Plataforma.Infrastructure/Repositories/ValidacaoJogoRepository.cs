using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.Infrastructure.Repositories;

public class ValidacaoJogoRepository : IValidacaoJogoRepository
{
    private readonly PlataformaDbContext _context;

    public ValidacaoJogoRepository(PlataformaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(ValidacaoJogo validacao)
    {
        _context.Validacoes.Add(validacao);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ValidacaoJogo>> ObterPorJogoAsync(Guid jogoId)
    {
        return await _context.Validacoes
            .Where(v => v.JogoId == jogoId)
            .ToListAsync();
    }

    public async Task<bool> JaValidouAsync(Guid jogoId, Guid jogadorId)
    {
        return await _context.Validacoes
            .AnyAsync(v => v.JogoId == jogoId && v.JogadorId == jogadorId);
    }
}