using Baked.Architecture;
using Baked.Configuration;
using Microsoft.Extensions.Configuration;

namespace Baked;

public static class ConfigurationExtensions
{
    public static void AddConfiguration(this List<ILayer> source) =>
        source.Add(new ConfigurationLayer());

    public static void ConfigureConfigurationBuilder(this LayerConfigurator source, Action<IConfigurationBuilder> configuration) =>
        source.Configure(configuration);
}