using Baked.Architecture;
using Baked.RateLimiter;

namespace Baked;

public static class RateLimiterExtensions
{
    extension(List<IFeature> features)
    {
        public void AddRateLimiter(Func<RateLimiterConfigurator, IFeature<RateLimiterConfigurator>> configure) =>
            features.Add(configure(new()));
    }
}