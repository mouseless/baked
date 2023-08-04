using NHibernate;

namespace Do.Orm.Default;

public class QueryContext<TEntity> : IQueryContext<TEntity>
{
    readonly Func<ISession> _getSession;

    public QueryContext(Func<ISession> getSession) =>
        _getSession = getSession;

    public TEntity SingleById(Guid id) => _getSession().Get<TEntity>(id);
    public IQueryable<TEntity> Query() => _getSession().Query<TEntity>();
}
