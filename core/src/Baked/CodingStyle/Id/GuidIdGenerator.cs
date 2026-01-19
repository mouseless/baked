using NHibernate.Engine;
using NHibernate.Id;

namespace Baked.CodingStyle.Id;

public class GuidIdGenerator : IIdentifierGenerator
{
    GuidGenerator _generator = new GuidGenerator();

    public object Generate(ISessionImplementor session, object obj)
    {
        return Business.Id.Parse(_generator.Generate(session, obj));
    }

    public Task<object> GenerateAsync(ISessionImplementor session, object obj, CancellationToken cancellationToken)
    {
        return Task.FromResult(Generate(session, obj));
    }
}