using Baked.Architecture;

namespace Baked;

public static class GreetingFeature
{
    public static void AddGreeting(this List<IFeature> features,
        FeatureFunc<Configurator> configure
    ) => features.Add(configure(new()));

    public class Configurator : FeatureConfigurator<Configurator>;
}