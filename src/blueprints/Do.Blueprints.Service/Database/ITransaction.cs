namespace Do.Database;

public interface ITransaction
{
    Task CommitAsync(Action action);
    Task<TEntity> CommitAsync<TEntity>(Func<TEntity> action);
    Task CommitAsync<TEntity>(TEntity entity, Action<TEntity> action);
}
