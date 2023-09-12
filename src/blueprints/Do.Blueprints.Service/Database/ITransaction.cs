namespace Do.Database;

public interface ITransaction
{
    Task CommitAsync(Action action);
    Task CommitAsync(Func<Task> action);

    Task<TEntity> CommitAsync<TEntity>(Func<TEntity> action);
    Task<TEntity> CommitAsync<TEntity>(Func<Task<TEntity>> action);

    Task CommitAsync<TEntity>(TEntity entity, Action<TEntity> action);
    Task CommitAsync<TEntity>(TEntity entity, Func<TEntity, Task> action);
}
