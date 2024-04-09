using Do.CodingStyle;
using Do.CodingStyle.UseBuiltInTypes;

namespace Do;

public static class UseBuiltInTypesCodingStyleExtensions
{
    public static UseBuiltInTypesCodingStyleFeature UseBuiltInTypes(this CodingStyleConfigurator _,
        IEnumerable<string>? textPropertySuffices = default
    ) => new(textPropertySuffices ?? ["Data"]);
}