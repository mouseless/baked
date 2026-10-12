using Baked.CodingStyle.RemainingServicesAreSingleton;

namespace Baked;

public static class RemainingServicesAreSingletonCodingStyleExtensions
{
    extension(CodingStyleFeature.Configurator _)
    {
        public RemainingServicesAreSingletonCodingStyleFeature RemainingServicesAreSingleton() =>
            new();
    }
}