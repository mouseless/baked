using Baked.Architecture;

namespace Baked;

public static class UxFeature
{
    public static void AddUx(this List<IFeature> features,
        IEnumerable<FeatureFunc<Configurator>> configures
    ) => features.AddRange(configures.Select(configure => configure(new())));

    public static void AddUxes(this List<IFeature> features,
        IEnumerable<FeatureFunc<Configurator>> configures
    ) => features.AddUx(configures);

    public class Configurator : FeatureConfigurator<Configurator>;
}