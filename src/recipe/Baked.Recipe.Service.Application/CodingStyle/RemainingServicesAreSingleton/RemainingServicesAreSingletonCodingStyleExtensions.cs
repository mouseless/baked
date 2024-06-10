using Baked.CodingStyle;
using Baked.CodingStyle.RemainingServicesAreSingleton;

namespace Baked;

public static class RemainingServicesAreSingletonCodingStyleExtensions
{
    public static RemainingServicesAreSingletonCodingStyleFeature RemainingServicesAreSingleton(this CodingStyleConfigurator _) =>
        new();
}