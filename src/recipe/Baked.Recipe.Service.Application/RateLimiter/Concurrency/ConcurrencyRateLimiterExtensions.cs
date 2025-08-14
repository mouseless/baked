using Baked.RateLimiter;
using Baked.RateLimiter.Concurrency;
using Baked.Runtime;

namespace Baked;

public static class ConcurrencyRateLimiterExtensions
{
    public static ConcurrencyRateLimiterFeature Concurrency(this RateLimiterConfigurator _,
        Setting<int>? permitLimit = default,
        Setting<int>? queueLimit = default
    ) => new(permitLimit, queueLimit);
}