using System;
using QNF.Plataforma.Application.Interfaces;

namespace QNF.Plataforma.Application.Handlers;

public abstract class CommandHandlerBase<TCommand, TResult> : ICommandHandler<TCommand, TResult>
{
    public async Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            await ValidateAsync(command, cancellationToken);

            LogCommand(command);

            using (var tx = BeginTransaction())
            {
                var result = await ExecuteAsync(command, cancellationToken);

                await PublishDomainEventsAsync(cancellationToken);

                CommitTransaction(tx);
                return result;
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
            throw; 
        }
    }

    protected abstract Task<TResult> ExecuteAsync(TCommand command, CancellationToken cancellationToken);

    protected virtual Task ValidateAsync(TCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;

    protected virtual void LogCommand(TCommand command) { }

    protected virtual IDisposable BeginTransaction() => new NoOpTransaction();

    protected virtual void CommitTransaction(IDisposable transaction) => transaction.Dispose();

    protected virtual Task PublishDomainEventsAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    protected virtual void HandleException(Exception ex) { }

    private class NoOpTransaction : IDisposable
    {
        public void Dispose() { }
    }
}
