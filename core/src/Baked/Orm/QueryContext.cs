using NHibernate;

namespace Baked.Orm;

public class QueryContext<TEntity>(Func<ISession> _getSession, IManyToOneFetcher<TEntity> _fetcher)
    : IQueryContext<TEntity>
{
    public IQueryable<TEntity> Query(bool fetchParents = true) =>
        fetchParents
            ? _fetcher.Fetch(_getSession().Query<TEntity>())
            : _getSession().Query<TEntity>();
}