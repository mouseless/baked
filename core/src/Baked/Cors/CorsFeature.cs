using Baked.Architecture;

namespace Baked;

public static class CorsFeature
{
    public static void AddCors(this List<IFeature> features,
        FeatureFunc<Configurator> configure
    ) => features.Add(configure(new()));

    public class Configurator : FeatureConfigurator<Configurator>;
}