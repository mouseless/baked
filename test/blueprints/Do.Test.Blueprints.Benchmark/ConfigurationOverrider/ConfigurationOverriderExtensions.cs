using Do.Architecture;
using Do.Test.ConfigurationOverrider;

namespace Do;

public static class ConfigurationOverriderExtensions
{
    public static void AddConfigurationOverrider(this List<IFeature> source) => source.Add(new ConfigurationOverriderFeature());
}
