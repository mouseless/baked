using Baked.Playground.CodingStyle.RichTransient;

namespace Baked.Playground.CodingStyle.LocatableExtension;

public class RichTransientAsyncExtension
{
    RichTransientAsync _richTransientAsync = default!;

    public Baked.Business.Id Id => _richTransientAsync.Id;

    internal RichTransientAsyncExtension With(RichTransientAsync richTransientAsync)
    {
        _richTransientAsync = richTransientAsync;

        return this;
    }

    public string FromExtension() =>
        $"This method is from extension for {nameof(RichTransientAsync)}:{_richTransientAsync.Id}";
}