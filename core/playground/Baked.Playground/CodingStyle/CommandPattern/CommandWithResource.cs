using Baked.Playground.CodingStyle.ResourceViaIdInitializer;

namespace Baked.Playground.CodingStyle.CommandPattern;

public class CommandWithResource
{
    ResourceWithData _transient = default!;

    public CommandWithResource With(ResourceWithData transient)
    {
        _transient = transient;

        return this;
    }

    public ResourceWithData Execute() =>
        _transient;
}