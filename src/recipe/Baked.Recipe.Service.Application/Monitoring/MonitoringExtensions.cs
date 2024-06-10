using Baked.Architecture;
using Baked.Logging;
using Microsoft.Extensions.Logging;

namespace Baked;

public static class MonitoringExtensions
{
    public static void AddMonitoring(this List<ILayer> source) =>
        source.Add(new MonitoringLayer());

    public static void ConfigureLoggingBuilder(this LayerConfigurator source, Action<ILoggingBuilder> configuration) =>
        source.Configure(configuration);
}