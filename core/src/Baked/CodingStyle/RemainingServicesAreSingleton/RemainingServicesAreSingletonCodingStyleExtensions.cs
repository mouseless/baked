using Baked.CodingStyle;
using Baked.CodingStyle.RemainingServicesAreSingleton;

namespace Baked;

public static class RemainingServicesAreSingletonCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public RemainingServicesAreSingletonCodingStyleFeature RemainingServicesAreSingleton() =>
            new();
    }
}