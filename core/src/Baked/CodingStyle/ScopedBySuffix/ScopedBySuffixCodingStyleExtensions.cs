using Baked.CodingStyle;
using Baked.CodingStyle.ScopedBySuffix;

namespace Baked;

public static class ScopedBySuffixCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public ScopedBySuffixCodingStyleFeature ScopedBySuffix(
            IEnumerable<string>? suffixes = default
        ) => new(suffixes ?? ["Context"]);
    }
}