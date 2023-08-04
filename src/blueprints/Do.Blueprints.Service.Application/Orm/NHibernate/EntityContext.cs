using ISession = NHibernate.ISession;

namespace Do.Orm.NHibernate;

public class EntityContext<TEntity> : IEntityContext<TEntity>
{
    readonly ISession _session;

    public EntityContext(ISession session) =>
        _session = session;

    public TEntity Insert(TEntity entity)
    {
        _session.Save(entity);

        return entity;
    }
}
