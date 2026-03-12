using Baked.Architecture;
using Baked.Logging;

namespace Baked;

public static class LoggingExtensions
{
    extension(List<IFeature> features)
    {
        public void AddLogging(Func<LoggingConfigurator, IFeature<LoggingConfigurator>> configure) =>
            features.Add(configure(new()));
    }
}