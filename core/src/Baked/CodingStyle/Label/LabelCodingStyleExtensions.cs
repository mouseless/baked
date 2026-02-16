using Baked.CodingStyle;
using Baked.CodingStyle.Label;

namespace Baked;

public static class LabelCodingStyleExtensions
{
    public static LabelCodingStyleFeature Label(this CodingStyleConfigurator _,
        IEnumerable<string>? propertyNames = default
    )
    {
        propertyNames ??= ["Display", "Label", "Name", "Title"];

        return new(propertyNames);
    }
}