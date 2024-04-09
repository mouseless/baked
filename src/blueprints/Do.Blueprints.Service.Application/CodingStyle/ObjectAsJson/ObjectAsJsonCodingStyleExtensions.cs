using Do.CodingStyle;
using Do.CodingStyle.ObjectAsJson;

namespace Do;

public static class ObjectAsJsonCodingStyleExtensions
{
    public static ObjectAsJsonCodingStyleFeature ObjectAsJson(this CodingStyleConfigurator _) =>
        new();
}
