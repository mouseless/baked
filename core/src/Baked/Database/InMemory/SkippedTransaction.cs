namespace Baked.Database.InMemory;

public class SkippedTransaction : ITransaction
{
    public Task CommitAsync(Action action)
    {
        action();

        return Task.CompletedTask;
    }

    public async Task CommitAsync(Func<Task> action)
    {
        await action();
    }

    public Task<T> CommitAsync<T>(Func<T> action)
    {
        var result = action();

        return Task.FromResult(result);
    }

    public async Task<TEntity> CommitAsync<TEntity>(Func<Task<TEntity>> action)
    {
        return await action();
    }

    public Task CommitAsync<TEntity>(TEntity? entity, Action<TEntity> action)
    {
        if (entity is null) { return Task.CompletedTask; }

        action(entity);

        return Task.CompletedTask;
    }

    public async Task CommitAsync<TEntity>(TEntity? entity, Func<TEntity, Task> action)
    {
        if (entity is null) { return; }

        await action(entity);
    }
}