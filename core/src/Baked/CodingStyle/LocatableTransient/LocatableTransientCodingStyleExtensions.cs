using Baked.CodingStyle;
using Baked.CodingStyle.LocatableTransient;

namespace Baked;

public static class LocatableTransientCodingStyleExtensions
{
    public static LocatableTransientCodingStyleFeature LocatableTransient(this CodingStyleConfigurator _) =>
        new();
}
