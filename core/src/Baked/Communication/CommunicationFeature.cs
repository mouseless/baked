using Baked.Architecture;

namespace Baked;

public static class CommunicationFeature
{
    public static void AddCommunication(this List<IFeature> features,
        FeatureFunc<Configurator> configure
    ) => features.Add(configure(new()));

    public class Configurator : FeatureConfigurator<Configurator>;
}