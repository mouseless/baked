using Baked.CodingStyle;
using Baked.CodingStyle.NameBasedLabel;

namespace Baked;

public static class NameBasedLabelCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public NameBasedLabelCodingStyleFeature NameBasedLabel(
            IEnumerable<string>? propertyNames = default
        )
        {
            propertyNames ??= ["Display", "Label", "Name", "Title"];

            return new(propertyNames);
        }
    }
}