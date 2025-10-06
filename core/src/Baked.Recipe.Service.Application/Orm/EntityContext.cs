using NHibernate;

namespace Baked.Orm;

public class EntityContext<TEntity>(ISession _session)
    : IEntityContext<TEntity>
{
    public TEntity Insert(TEntity entity)
    {
        _session.Save(entity);

        return entity;
    }

    public void Delete(TEntity entity)
    {
        _session.Delete(entity);
    }

    public void Lock(TEntity entity)
    {
        _session.Refresh(entity, LockMode.Upgrade);
    }
}