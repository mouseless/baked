using Baked.Architecture;

namespace Baked;

public static class CodingStyleFeature
{
    public static void AddCodingStyles(this List<IFeature> features,
        IEnumerable<FeatureFunc<Configurator>> configures
    ) => features.AddRange(configures.Select(configure => configure(new())));

    public class Configurator : FeatureConfigurator<Configurator>;
}