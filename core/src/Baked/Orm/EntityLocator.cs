using Baked.Business;
using NHibernate;

namespace Baked.Orm;

public class EntityLocator<TEntity>(Func<ISession> _getSession)
    : ILocator<TEntity>
{
    public TEntity Locate(Business.Id id, bool throwNotFound) =>
        _getSession().Get<TEntity>(id) ?? throw RecordNotFoundException.For<TEntity>(id, throwNotFound);

    public IEnumerable<TEntity> LocateMany(IEnumerable<Business.Id> ids) =>
        [.. ids.Select(id => Locate(id, false))];
}