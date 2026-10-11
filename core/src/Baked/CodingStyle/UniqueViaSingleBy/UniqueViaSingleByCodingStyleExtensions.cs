using Baked.CodingStyle;
using Baked.CodingStyle.UniqueViaSingleBy;

namespace Baked;

public static class UniqueViaSingleByCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public UniqueViaSingleByCodingStyleFeature UniqueViaSingleBy() =>
            new();
    }
}