using Baked.CodingStyle;
using Baked.CodingStyle.FlagsEnum;

namespace Baked;

public static class FlagsEnumCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public FlagsEnumCodingStyleFeature FlagsEnum() =>
            new();
    }
}