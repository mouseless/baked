using Baked.Architecture;
using Baked.Logging;

namespace Baked;

public static class LoggingExtensions
{
    public static void AddLogging(this List<IFeature> source, Func<LoggingConfigurator, IFeature<LoggingConfigurator>> configure) =>
        source.Add(configure(new()));
}