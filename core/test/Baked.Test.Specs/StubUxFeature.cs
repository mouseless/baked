using Baked.Testing;
using Baked.Theme;

namespace Baked.Test;

public class StubUxFeature(Stubber giveMe)
{
    public TSchema Configure<TSchema>(Func<TSchema> create)
    {
        var inspect = Inspect.TraceHere();

        return inspect.Capture(giveMe.AComponentContext(), create);
    }
}