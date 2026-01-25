using Baked.Business;
using Baked.Orm;

namespace Baked.CodingStyle.RichEntity;

public class EntityLocator<T>(IQueryContext<T> _queryContext) : ILocator<T>
{
    public IEnumerable<T> Multiple(IEnumerable<Business.Id> ids) =>
        _queryContext.ByIds(ids);

    public T Single(Business.Id id, bool throwNotFound) =>
        _queryContext.SingleById(id, throwNotFound: throwNotFound);
}