using Baked.Business;
using Baked.Orm;

namespace Baked.CodingStyle.RichEntity;

public class EntityLocator<T>(IQueryContext<T> _queryContext)
    : ILocator<T>
{
    public T Locate(Business.Id id, bool throwNotFound) =>
        _queryContext.SingleById(id, throwNotFound: throwNotFound);

    public IEnumerable<T> LocateMany(IEnumerable<Business.Id> ids) =>
        _queryContext.ByIds(ids);
}