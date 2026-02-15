using Baked.Business;
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

    public static implicit operator RichTransientAsyncExtension(RichTransientAsync other) =>
        other.Cast().To<RichTransientAsyncExtension>();
}

public class RichTransientAsyncExtensions(Func<RichTransientAsyncExtension> _newRichTransientAsyncExtension)
    : ICasts<RichTransientAsync, RichTransientAsyncExtension>
{
    RichTransientAsyncExtension ICasts<RichTransientAsync, RichTransientAsyncExtension>.To(RichTransientAsync from) =>
        _newRichTransientAsyncExtension().With(from);
}