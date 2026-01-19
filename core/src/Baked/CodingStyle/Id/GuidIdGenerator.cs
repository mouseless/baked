using NHibernate.Engine;
using NHibernate.Id;

namespace Baked.CodingStyle.Id;

public class GuidIdGenerator : GuidGenerator, IIdentifierGenerator
{
    public new object Generate(ISessionImplementor session, object obj) =>
        Business.Id.Parse(base.Generate(session, obj));

    public new async Task<object> GenerateAsync(ISessionImplementor session, object obj, CancellationToken cancellationToken) =>
        Business.Id.Parse(await base.GenerateAsync(session, obj, cancellationToken));
}