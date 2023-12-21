using ISession = NHibernate.ISession;

namespace Do.Orm.Default;

public class EntityContext<TEntity>(ISession _session) : IEntityContext<TEntity>
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
}
