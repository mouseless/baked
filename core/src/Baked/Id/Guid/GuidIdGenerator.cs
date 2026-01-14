using NHibernate.Engine;
using NHibernate.Id;

namespace Baked.Id.Guid;

public class GuidIdGenerator : IIdentifierGenerator
{
    public object Generate(ISessionImplementor session, object obj)
    {
        return Orm.Id.Parse(System.Guid.NewGuid());
    }

    public Task<object> GenerateAsync(ISessionImplementor session, object obj, CancellationToken cancellationToken)
    {
        return Task.FromResult(Generate(session, obj));
    }
}