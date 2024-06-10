using Do.CodingStyle;
using Do.CodingStyle.ScopedBySuffix;

namespace Do;

public static class ScopedBySuffixCodingStyleExtensions
{
    public static ScopedBySuffixCodingStyleFeature ScopedBySuffix(this CodingStyleConfigurator _,
        IEnumerable<string>? suffixes = default
    ) => new(suffixes ?? ["Context"]);
}