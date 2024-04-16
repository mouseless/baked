using Do.CodingStyle;
using Do.CodingStyle.RemainingServicesAreSingleton;

namespace Do;

public static class RemainingServicesAreSingletonCodingStyleExtensions
{
    public static RemainingServicesAreSingletonCodingStyleFeature RemainingServicesAreSingleton(this CodingStyleConfigurator _) =>
        new();
}