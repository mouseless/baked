using Baked.Architecture;
using Baked.Configuration;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Baked;

public static class ConfigurationExtensions
{
    public static void AddConfiguration(this List<ILayer> source) =>
        source.Add(new ConfigurationLayer());

    public static void ConfigureConfigurationBuilder(this LayerConfigurator source, Action<IConfigurationBuilder> configuration) =>
        source.Configure(configuration);

    public static void AddJson(this IConfigurationBuilder configuration, string json) =>
        configuration.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)));
}