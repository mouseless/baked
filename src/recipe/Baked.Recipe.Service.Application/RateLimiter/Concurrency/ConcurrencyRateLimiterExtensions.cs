using Baked.RateLimiter;
using Baked.RateLimiter.Concurrency;

namespace Baked;

public static class ConcurrencyRateLimiterExtensions
{
    public static ConcurrencyRateLimiterFeature Concurrency(this RateLimiterConfigurator _,
        int? permitLimit = default,
        int? queueLimit = default
    ) => new(new() { PermitLimit = permitLimit ?? 5, QueueLimit = queueLimit ?? 10 });
}