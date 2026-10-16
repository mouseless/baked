using Baked.Architecture;

namespace Baked;

public static class LoggingFeature
{
    public static void AddLogging(this List<IFeature> features,
        FeatureFunc<Configurator> configure
    ) => features.Add(configure(new()));

    public class Configurator : FeatureConfigurator<Configurator>;
}