using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}