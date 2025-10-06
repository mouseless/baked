using Baked.Architecture;

namespace Baked.RateLimiter;

public class RateLimiterConfigurator
{
    public IFeature<RateLimiterConfigurator> Disabled() =>
        Feature.Empty<RateLimiterConfigurator>();
}