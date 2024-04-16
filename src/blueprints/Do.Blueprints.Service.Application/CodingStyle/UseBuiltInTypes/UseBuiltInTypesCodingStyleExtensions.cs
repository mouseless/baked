using Do.CodingStyle;
using Do.CodingStyle.UseBuiltInTypes;

namespace Do;

public static class UseBuiltInTypesCodingStyleExtensions
{
    public static UseBuiltInTypesCodingStyleFeature UseBuiltInTypes(this CodingStyleConfigurator _,
        IEnumerable<string>? textPropertySuffixes = default
    ) => new(textPropertySuffixes ?? ["Data"]);
}