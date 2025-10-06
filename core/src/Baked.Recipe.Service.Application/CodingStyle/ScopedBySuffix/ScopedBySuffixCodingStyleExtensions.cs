using Baked.CodingStyle;
using Baked.CodingStyle.ScopedBySuffix;

namespace Baked;

public static class ScopedBySuffixCodingStyleExtensions
{
    public static ScopedBySuffixCodingStyleFeature ScopedBySuffix(this CodingStyleConfigurator _,
        IEnumerable<string>? suffixes = default
    ) => new(suffixes ?? ["Context"]);
}