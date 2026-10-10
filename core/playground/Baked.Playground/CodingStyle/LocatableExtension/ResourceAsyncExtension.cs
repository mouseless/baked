using Baked.Playground.CodingStyle.ResourceViaIdInitializer;

namespace Baked.Playground.CodingStyle.LocatableExtension;

public class ResourceAsyncExtension
{
    ResourceAsync _richTransientAsync = default!;

    public Baked.Business.Id Id => _richTransientAsync.Id;

    internal ResourceAsyncExtension With(ResourceAsync richTransientAsync)
    {
        _richTransientAsync = richTransientAsync;

        return this;
    }

    public string FromExtension() =>
        $"This method is from extension for {nameof(ResourceAsync)}:{_richTransientAsync.Id}";
}