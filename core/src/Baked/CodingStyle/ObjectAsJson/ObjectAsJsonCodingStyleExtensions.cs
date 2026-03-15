using Baked.CodingStyle;
using Baked.CodingStyle.ObjectAsJson;

namespace Baked;

public static class ObjectAsJsonCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public ObjectAsJsonCodingStyleFeature ObjectAsJson() =>
            new();
    }
}