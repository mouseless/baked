namespace Do.Orm;

public interface IEntityContext<TEntity>
{
    TEntity Insert(TEntity entity);
}
