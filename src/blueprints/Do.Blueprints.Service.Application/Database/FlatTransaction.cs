using Microsoft.Extensions.Logging;
using NHibernate;

namespace Do.Database;

public class FlatTransaction : ITransaction
{
    readonly Func<ISession> _getSession;
    readonly ILogger<FlatTransaction> _logger;

    public FlatTransaction(Func<ISession> getSession, ILogger<FlatTransaction> log) =>
        (_getSession, _logger) = (getSession, log);

    public async Task<T> CommitAsync<T>(Func<T> action)
    {
        var result = action();

        await CommitAndBeginNewTransaction();

        return result;
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
