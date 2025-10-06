using Baked.Caching;
using Baked.Caching.ScopedMemory;
using Baked.Runtime;
using Baked.Testing;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;

namespace Baked;

public static class ScopedMemoryCachingExtensions
{
    public static ScopedMemoryCachingFeature ScopedMemory(this CachingConfigurator _,
        Setting<TimeSpan>? clientExpiration = default
    ) => new(clientExpiration ?? TimeSpan.FromHours(1));

    public static IMemoryCache AMemoryCache(this Stubber giveMe,
        bool clear = false
    )
    {
        var getMemoryCache = giveMe.The<Func<IMemoryCache>>();
        var memoryCache = getMemoryCache();

        if (clear)
        {
            if (memoryCache is not MemoryCache concreteMemoryCache) { throw new AssertionException("Cache cannot be cleared because it is not a `MemoryCache` instance"); }

            concreteMemoryCache.Clear();
        }

        return memoryCache;
    }
}