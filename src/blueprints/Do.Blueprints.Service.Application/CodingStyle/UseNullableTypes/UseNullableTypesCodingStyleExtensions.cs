using Do.CodingStyle;
using Do.CodingStyle.UseNullableTypes;

namespace Do;

public static class UseNullableTypesCodingStyleExtensions
{
    public static UseNullableTypesCodingStyleFeature UseNullableTypes(this CodingStyleConfigurator _) =>
        new();
}