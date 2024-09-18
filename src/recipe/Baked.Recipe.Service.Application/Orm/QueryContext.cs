using NHibernate;
using NHibernate.Linq;
using NHibernate.Type;
using System.Linq.Expressions;

namespace Baked.Orm;

public class QueryContext<TEntity>(Func<ISession> _getSession, NHibernate.Cfg.Configuration _configuration)
    : IQueryContext<TEntity>
{
    public TEntity SingleById(Guid id,
        bool throwNotFound = false
    ) => _getSession().Get<TEntity>(id) ?? throw RecordNotFoundException.For<TEntity>(id, throwNotFound);

    public IQueryable<TEntity> Query(bool fetchParents = true)
    {
        var result = _getSession().Query<TEntity>();

        if (!fetchParents) { return result; }

        var parents = _configuration
            .GetClassMapping(typeof(TEntity))
            .ReferenceablePropertyIterator
            .Where(p => p.Type is ManyToOneType);
        foreach (var parent in parents)
        {
            var paramExp = Expression.Parameter(typeof(TEntity)); // e =>
            var propExp = Expression.Property(paramExp, parent.Name); // e.Parent
            var fetchExp = Expression.Lambda(propExp, paramExp); // e => e.Parent
            var fetch = // .Fetch(e => e.Parent)
                typeof(EagerFetchingExtensionMethods)
                    .GetMethod(nameof(EagerFetchingExtensionMethods.Fetch))
                    ?.MakeGenericMethod(typeof(TEntity), parent.Type.ReturnedClass)
                ?? throw new("Fetch extension should've existed");

            result = (IQueryable<TEntity>?)fetch.Invoke(null, new object[] { result, fetchExp })
                ?? throw new("Fetch extension should've returned not null");
        }

        return result;
    }
}