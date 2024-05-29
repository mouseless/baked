namespace Do.Orm;

public interface IEntityContext<TEntity>
{
    TEntity Insert(TEntity entity);
    void Delete(TEntity entity);
    void Lock(TEntity entity);
}