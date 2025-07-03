using Microsoft.EntityFrameworkCore;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly WriteDbContext _writeContext;
    private readonly ReadDbContext _readContext;

    public GameRepository(WriteDbContext writeContext, ReadDbContext readContext)
    {
        _writeContext = writeContext;
        _readContext = readContext;
    }

    public async Task AddAsync(Game game)
    {
        _writeContext.Games.Add(game);
        await _writeContext.SaveChangesAsync();
    }

    public async Task<Game> GetByIdAsync(Guid id)
    {
        return await _readContext.Games
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task UpdateAsync(Game game)
    {
        _writeContext.Games.Update(game);
        await _writeContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var game = await _writeContext.Games.FindAsync(id);
        if (game != null)
        {
            _writeContext.Games.Remove(game);
            await _writeContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _readContext.Games.AsNoTracking().ToListAsync();
    }
}