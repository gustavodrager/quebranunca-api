namespace QNF.Plataforma.Core.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}