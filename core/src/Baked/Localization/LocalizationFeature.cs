using Baked.Architecture;

namespace Baked;

public static class LocalizationFeature
{
    public static void AddLocalization(this List<IFeature> features,
        FeatureFunc<Configurator> configure
    ) => features.Add(configure(new()));

    public class Configurator : FeatureConfigurator<Configurator>;
}