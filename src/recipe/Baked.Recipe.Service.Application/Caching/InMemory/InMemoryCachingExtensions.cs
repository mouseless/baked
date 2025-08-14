using Baked.Caching;
using Baked.Caching.InMemory;
using Baked.Runtime;
using Baked.Testing;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;

namespace Baked;

public static class InMemoryCachingExtensions
{
    public static InMemoryCachingFeature InMemory(this CachingConfigurator _,
        Action<MemoryCacheOptions>? options = default,
        Setting<TimeSpan>? clientExpiration = default
    ) => new(options ?? (_ => { }), clientExpiration ?? TimeSpan.FromHours(1));

    public static IMemoryCache TheMemoryCache(this Stubber giveMe,
        bool clear = false
    )
    {
        var memoryCache = giveMe.The<IMemoryCache>();

        if (clear)
        {
            if (memoryCache is not MemoryCache concreteMemoryCache) { throw new AssertionException("Cache cannot be cleared because it is not a `MemoryCache` instance"); }

            concreteMemoryCache.Clear();
        }

        return memoryCache;
    }
}