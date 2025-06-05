using Baked.Architecture;
using Baked.RateLimiter;

namespace Baked;

public static class RateLimiterExtensions
{
    public static void AddRateLimiter(this List<IFeature> features, Func<RateLimiterConfigurator, IFeature<RateLimiterConfigurator>> configure) =>
       features.Add(configure(new()));
}