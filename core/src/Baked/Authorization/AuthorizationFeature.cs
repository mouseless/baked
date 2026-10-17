using Baked.Architecture;

namespace Baked;

public static class AuthorizationFeature
{
    public static void AddAuthorization(this List<IFeature> features,
        FeatureFunc<Configurator> configure
    ) => features.Add(configure(new()));

    public class Configurator : FeatureConfigurator<Configurator>;
}