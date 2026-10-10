using Baked.CodingStyle;
using Baked.CodingStyle.PrimitiveViaParsable;

namespace Baked;

public static class PrimitiveViaParsableCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public PrimitiveViaParsableCodingStyleFeature PrimitiveViaParsable() =>
            new();
    }
}