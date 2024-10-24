using Baked.CodingStyle;
using Baked.CodingStyle.RichTransient;

namespace Baked;

public static class RichTransientCodingStyleExtensions
{
    public static RichTransientCodingStyleFeature RichTransient(this CodingStyleConfigurator _) =>
        new();
}