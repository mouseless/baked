using Baked.CodingStyle.ScopedViaSuffix;

namespace Baked;

public static class ScopedViaSuffixCodingStyleExtensions
{
    extension(CodingStyleFeature.Configurator _)
    {
        public ScopedViaSuffixCodingStyleFeature ScopedViaSuffix(
            IEnumerable<string>? suffixes = default
        ) => new(suffixes ?? ["Context"]);
    }
}