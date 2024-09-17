using Baked.Architecture;
using Baked.Configuration;
using Microsoft.Extensions.Configuration;

namespace Baked;

public static class ConfigurationExtensions
{
    public static void AddConfiguration(this List<ILayer> layers) =>
        layers.Add(new ConfigurationLayer());

    public static void ConfigureConfigurationBuilder(this LayerConfigurator configurator, Action<IConfigurationBuilder> configuration) =>
        configurator.Configure(configuration);

    public static void AddJson(this IConfigurationBuilder builder, string json) =>
        builder.Add(new JsonConfigurationSource(json));

    public static void AddJsonAsDefault(this IConfigurationBuilder builder, string json) =>
        builder.Sources.Insert(
            Math.Max(builder.Sources.Count - 4, 0), // try to insert before appsetttings.json + appsettings.[Environment].json + 2 more default configurations
            new JsonConfigurationSource(json)
        );
}