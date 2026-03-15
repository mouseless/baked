using Baked.Architecture;
using Baked.Logging;

namespace Baked;

public static class LoggingExtensions
{
    extension(List<IFeature> features)
    {
        public void AddLogging(FeatureFunc<LoggingConfigurator> configure) =>
            features.Add(configure(new()));
    }
}