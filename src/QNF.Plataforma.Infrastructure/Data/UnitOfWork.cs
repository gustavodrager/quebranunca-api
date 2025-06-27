using QNF.Plataforma.Core.Interfaces;
using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly PlataformaDbContext _context;

    public UnitOfWork(PlataformaDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
