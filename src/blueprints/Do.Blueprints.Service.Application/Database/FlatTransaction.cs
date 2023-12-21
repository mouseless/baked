using Microsoft.Extensions.Logging;
using NHibernate;

namespace Do.Database;

public class FlatTransaction(Func<ISession> _getSession, ILogger<FlatTransaction> _logger) : ITransaction
{
    public async Task CommitAsync(Action action)
    {
        action();

        await CommitAndBeginNewTransaction();
    }

    public async Task CommitAsync(Func<Task> action)
    {
        await action();

        await CommitAndBeginNewTransaction();
    }

    public async Task<T> CommitAsync<T>(Func<T> action)
    {
        var result = action();

        await CommitAndBeginNewTransaction();

        return result;
    }

    public async Task<TEntity> CommitAsync<TEntity>(Func<Task<TEntity>> action)
    {
        var result = await action();

        await CommitAndBeginNewTransaction();

        return result;
    }

    public async Task CommitAsync<TEntity>(TEntity? entity, Action<TEntity> action)
    {
        if (entity is null) { return; }

        action(entity);

        await CommitAndBeginNewTransaction();
    }

    public async Task CommitAsync<TEntity>(TEntity? entity, Func<TEntity, Task> action)
    {
        if (entity is null) { return; }

        await action(entity);

        await CommitAndBeginNewTransaction();
    }

    async Task CommitAndBeginNewTransaction()
    {
        _logger.LogDebug("Committing current transaction...");

        var session = _getSession();
        var transaction = session.GetCurrentTransaction();

        await transaction.CommitAsync();
        transaction.Dispose();

        _logger.LogDebug("Current transaction committed.");

        session.BeginTransaction();
    }
}
