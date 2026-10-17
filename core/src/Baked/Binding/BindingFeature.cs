using Baked.Architecture;

namespace Baked;

public static class BindingFeature
{
    public static void AddBindings(this List<IFeature> features,
        params IEnumerable<FeatureFunc<Configurator>> configures
    ) => features.AddRange(configures.Select(configure => configure(new())));

    public class Configurator : FeatureConfigurator<Configurator>;
}