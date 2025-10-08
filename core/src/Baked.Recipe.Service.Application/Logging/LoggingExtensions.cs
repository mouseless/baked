using Baked.Architecture;
using Baked.Logging;

namespace Baked;

public static class LoggingExtensions
{
    public static void AddLogging(this List<IFeature> features, Func<LoggingConfigurator, IFeature<LoggingConfigurator>> configure) =>
        features.Add(configure(new()));
}