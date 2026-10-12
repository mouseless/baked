using Baked.Architecture;

namespace Baked;

public static class ExceptionHandlingFeature
{
    public static void AddExceptionHandling(this List<IFeature> features,
        FeatureFunc<Configurator> configure
    ) => features.Add(configure(new()));

    public class Configurator : FeatureConfigurator<Configurator>;
}