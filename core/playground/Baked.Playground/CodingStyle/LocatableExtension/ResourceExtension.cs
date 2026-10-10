using Baked.Playground.CodingStyle.ResourceViaIdInitializer;

namespace Baked.Playground.CodingStyle.LocatableExtension;

public class ResourceExtension
{
    ResourceWithData _richTransient = default!;

    public Baked.Business.Id Id => _richTransient.Id;

    internal ResourceExtension With(ResourceWithData richTransient)
    {
        _richTransient = richTransient;

        return this;
    }

    public string FromExtension() =>
        $"This method is from extension for {nameof(ResourceWithData)}:{_richTransient.Id}";
}