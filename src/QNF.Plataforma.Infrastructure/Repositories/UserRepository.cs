using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data; 
using Microsoft.EntityFrameworkCore;


namespace QNF.Plataforma.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly WriteDbContext _writeContext;
    private readonly ReadDbContext _readContext;

    public UserRepository(WriteDbContext writeContext, ReadDbContext readContext)
    {
        _writeContext = writeContext;
        _readContext = readContext;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _readContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _readContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O e-mail é obrigatório.", nameof(email));

        return await _readContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _writeContext.Users.AddAsync(user);
        await _writeContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _writeContext.Users.Update(user);
        await _writeContext.SaveChangesAsync();
    }
}
