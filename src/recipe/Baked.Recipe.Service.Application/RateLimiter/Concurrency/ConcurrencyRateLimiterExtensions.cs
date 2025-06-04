using Baked.RateLimiter;
using Baked.RateLimiter.Concurrency;
using System.Threading.RateLimiting;

namespace Baked;

public static class ConcurrencyRateLimiterExtensions
{
    public static ConcurrencyRateLimiterFeature Concurrency(this RateLimiterConfigurator _,
        ConcurrencyLimiterOptions? options = default
    ) => new(options ?? new() { PermitLimit = 5, QueueLimit = 10 });
}