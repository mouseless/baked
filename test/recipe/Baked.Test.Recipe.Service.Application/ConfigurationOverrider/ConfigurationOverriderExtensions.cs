using Baked.Architecture;
using Baked.Test.ConfigurationOverrider;

namespace Baked;

public static class ConfigurationOverriderExtensions
{
    public static void AddConfigurationOverrider(this List<IFeature> features) =>
        features.Add(new ConfigurationOverriderFeature());
}