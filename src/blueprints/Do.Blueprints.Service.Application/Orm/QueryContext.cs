using NHibernate;

namespace Do.Orm;

public class QueryContext<TEntity>(Func<ISession> _getSession)
    : IQueryContext<TEntity>
{
    public TEntity SingleById(Guid id,
        bool throwNotFound = false
    ) => _getSession().Get<TEntity>(id) ?? throw RecordNotFoundException.For<TEntity>(id, throwNotFound);

    public IQueryable<TEntity> Query() =>
        _getSession().Query<TEntity>();
}