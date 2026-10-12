using Baked.RateLimiter.Concurrency;
using Baked.Runtime;

namespace Baked;

public static class ConcurrencyRateLimiterExtensions
{
    extension(RateLimiterFeature.Configurator _)
    {
        public ConcurrencyRateLimiterFeature Concurrency(
            Setting<int>? permitLimit = default,
            Setting<int>? queueLimit = default
        ) => new(permitLimit, queueLimit);
    }
}