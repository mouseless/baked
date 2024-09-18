namespace Baked.Orm;

public interface IManyToOneFetcher<TEntity>
{
    IQueryable<TEntity> Fetch(IQueryable<TEntity> query);
}