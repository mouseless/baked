using Baked.Domain.Configuration;
using Baked.Domain.Inspection;

namespace Baked.Test.Domain;

public class StubFeature(DomainModelContext c)
{
    readonly Trace _trace = Trace.Here();

    public TSchema Configure<TSchema>(Func<TSchema> create,
        string? orderInfo = default
    ) =>
        _trace.CaptureAttribute(c, create, orderInfo: orderInfo);
}