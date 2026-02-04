using Baked.Business;
using Baked.Playground.CodingStyle.RichTransient;

namespace Baked.Playground.CodingStyle.LocatableExtension;

public class RichTransientExtension
{
    RichTransientWithData _richTransient = default!;

    internal RichTransientExtension With(RichTransientWithData richTransient)
    {
        _richTransient = richTransient;

        return this;
    }

    internal Baked.Business.Id Id => _richTransient.Id;

    public string FromExtension() =>
        $"This method is from extension for {nameof(RichTransientWithData)}:{_richTransient.Id}";

    public static implicit operator RichTransientExtension(RichTransientWithData entity) =>
        entity.Cast().To<RichTransientExtension>();
}

public class RichTransientExtensions(Func<RichTransientExtension> _newRichTransientExtension)
    : ICasts<RichTransientWithData, RichTransientExtension>
{
    RichTransientExtension ICasts<RichTransientWithData, RichTransientExtension>.To(RichTransientWithData from) =>
        _newRichTransientExtension().With(from);
}