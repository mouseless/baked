using Baked.CodingStyle;
using Baked.CodingStyle.ScopedViaSuffix;

namespace Baked;

public static class ScopedViaSuffixCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public ScopedViaSuffixCodingStyleFeature ScopedViaSuffix(
            IEnumerable<string>? suffixes = default
        ) => new(suffixes ?? ["Context"]);
    }
}