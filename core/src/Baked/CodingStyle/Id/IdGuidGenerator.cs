using NHibernate.Engine;
using NHibernate.Id;

namespace Baked.CodingStyle.Id;

public class IdGuidGenerator : GuidGenerator, IIdentifierGenerator
{
    public new object Generate(ISessionImplementor session, object obj) =>
        Business.Id.Create(base.Generate(session, obj));

    public new async Task<object> GenerateAsync(ISessionImplementor session, object obj, CancellationToken cancellationToken) =>
        Business.Id.Create(await base.GenerateAsync(session, obj, cancellationToken));

    object IIdentifierGenerator.Generate(ISessionImplementor session, object obj) =>
        Generate(session, obj);

    async Task<object> IIdentifierGenerator.GenerateAsync(ISessionImplementor session, object obj, CancellationToken cancellationToken) =>
        await GenerateAsync(session, obj, cancellationToken);
}