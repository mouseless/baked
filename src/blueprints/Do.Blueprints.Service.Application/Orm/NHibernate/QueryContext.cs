using NHibernate;

namespace Do.Orm.NHibernate;

public class QueryContext<TEntity> : IQueryContext<TEntity>
{
    readonly Func<ISession> _getSession;

    public QueryContext(Func<ISession> getSession) =>
        _getSession = getSession;

    public IQueryable<TEntity> Query() => _getSession().Query<TEntity>();
}
