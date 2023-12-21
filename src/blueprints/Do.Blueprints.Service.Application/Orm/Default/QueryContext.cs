using NHibernate;

namespace Do.Orm.Default;

public class QueryContext<TEntity>(Func<ISession> _getSession) : IQueryContext<TEntity>
{
    public TEntity SingleById(Guid id) => _getSession().Get<TEntity>(id) ?? throw RecordNotFoundException.For<TEntity>(id);
    public IQueryable<TEntity> Query() => _getSession().Query<TEntity>();
}
