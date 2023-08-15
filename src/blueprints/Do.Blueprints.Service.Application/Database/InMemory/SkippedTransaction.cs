namespace Do.Database.InMemory;

public class SkippedTransaction : ITransaction
{
    public Task CommitAsync(Action action)
    {
        action();

        return Task.CompletedTask;
    }

    public Task<T> CommitAsync<T>(Func<T> action)
    {
        var result = action();

        return Task.FromResult(result);
    }

    public Task CommitAsync<TEntity>(TEntity entity, Action<TEntity> action)
    {
        action(entity);

        return Task.CompletedTask;
    }
}
