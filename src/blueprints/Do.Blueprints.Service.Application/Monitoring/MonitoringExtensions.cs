using Do.Architecture;
using Do.Logging;
using Microsoft.Extensions.Logging;

namespace Do;

public static class MonitoringExtensions
{
    public static void AddMonitoring(this List<ILayer> source) => source.Add(new MonitoringLayer());
    public static void ConfigureLoggingBuilder(this LayerConfigurator source, Action<ILoggingBuilder> configuration) => source.Configure(configuration);
}
