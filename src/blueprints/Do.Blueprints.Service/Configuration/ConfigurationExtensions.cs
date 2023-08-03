using Do.Architecture;
using Do.Configuration;
using Microsoft.Extensions.Configuration;

namespace Do;

public static class ConfigurationExtensions
{
    public static void AddConfiguration(this List<ILayer> source) => source.Insert(0, new ConfigurationLayer());
    public static void ConfigureConfigurationBuilder(this LayerConfigurator source, Action<IConfigurationBuilder> configuration) => source.Configure(configuration);
}
