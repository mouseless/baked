using Baked.CodingStyle;
using Baked.CodingStyle.Label;

namespace Baked;

public static class LabelCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public LabelCodingStyleFeature Label(
            IEnumerable<string>? propertyNames = default
        )
        {
            propertyNames ??= ["Display", "Label", "Name", "Title"];

            return new(propertyNames);
        }
    }
}