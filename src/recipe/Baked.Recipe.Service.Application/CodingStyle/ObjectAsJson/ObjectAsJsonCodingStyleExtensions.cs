using Baked.CodingStyle;
using Baked.CodingStyle.ObjectAsJson;

namespace Baked;

public static class ObjectAsJsonCodingStyleExtensions
{
    public static ObjectAsJsonCodingStyleFeature ObjectAsJson(this CodingStyleConfigurator _) =>
        new();
}