using Baked.CodingStyle;
using Baked.CodingStyle.StatusEnumViaSuffix;

namespace Baked;

public static class StatusEnumViaSuffixCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public StatusEnumViaSuffixCodingStyleFeature StatusEnumViaSuffix() =>
            new();
    }
}