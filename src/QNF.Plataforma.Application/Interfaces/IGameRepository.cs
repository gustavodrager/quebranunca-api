using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Application.Interfaces;

public interface IGameRepository
{
    Task AddAsync(Game game);
    Task<Game> GetByIdAsync(Guid id);
    Task UpdateAsync(Game game);
    Task DeleteAsync(Guid id);
}