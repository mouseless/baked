using Baked.Business;
using NHibernate;

namespace Baked.Orm;

public class EntityLocator<TEntity>(Func<ISession> _getSession)
    : ILocator<TEntity>
{
    public TEntity Locate(Id id, bool throwNotFound) =>
        _getSession().Get<TEntity>(id) ?? throw RecordNotFoundException.For<TEntity>(id, throwNotFound);

    public LazyLocatable<TEntity> LocateLazily(Id id)
    {
        var result = _getSession().Load<TEntity>(id);

        return new(
            result,
            async () =>
            {
                try
                {
                    await NHibernateUtil.InitializeAsync(result);
                }
                catch (ObjectNotFoundException)
                {
                    throw RecordNotFoundException.For<TEntity>(id);
                }
            }
        );
    }

    public IEnumerable<TEntity> LocateMany(IEnumerable<Id> ids) =>
        [.. ids.Select(id => Locate(id, false))];
}