using Baked.Architecture;
using Baked.Logging;
using Microsoft.Extensions.Logging;

namespace Baked;

public static class MonitoringExtensions
{
    public static void AddMonitoring(this List<ILayer> layers) =>
        layers.Add(new MonitoringLayer());

    public static void ConfigureLoggingBuilder(this LayerConfigurator configurator, Action<ILoggingBuilder> configuration) =>
        configurator.Configure(configuration);
}