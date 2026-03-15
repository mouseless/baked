using Baked.Architecture;
using Baked.RateLimiter;

namespace Baked;

public static class RateLimiterExtensions
{
    extension(List<IFeature> features)
    {
        public void AddRateLimiter(FeatureFunc<RateLimiterConfigurator> configure) =>
            features.Add(configure(new()));
    }
}