using Baked.Architecture;

namespace Baked;

public static class RateLimiterFeature
{
    public static void AddRateLimiter(this List<IFeature> features,
        FeatureFunc<Configurator> configure
    ) => features.Add(configure(new()));

    public class Configurator : FeatureConfigurator<Configurator>;
}