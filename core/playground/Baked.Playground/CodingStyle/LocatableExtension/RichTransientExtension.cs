using Baked.Playground.CodingStyle.RichTransient;

namespace Baked.Playground.CodingStyle.LocatableExtension;

public class RichTransientExtension
{
    RichTransientWithData _richTransient = default!;

    public Baked.Business.Id Id => _richTransient.Id;

    internal RichTransientExtension With(RichTransientWithData richTransient)
    {
        _richTransient = richTransient;

        return this;
    }

    public string FromExtension() =>
        $"This method is from extension for {nameof(RichTransientWithData)}:{_richTransient.Id}";
}