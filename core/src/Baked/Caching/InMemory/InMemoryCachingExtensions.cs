using Baked.Caching;
using Baked.Caching.InMemory;
using Baked.Runtime;
using Baked.Testing;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;

namespace Baked;

public static class InMemoryCachingExtensions
{
    extension(CachingConfigurator _)
    {
        public InMemoryCachingFeature InMemory(
            Action<MemoryCacheOptions>? options = default,
            Setting<TimeSpan>? clientExpiration = default
        ) => new(options ?? (_ => { }), clientExpiration ?? TimeSpan.FromHours(1));
    }

    extension(Stubber giveMe)
    {
        public IMemoryCache TheMemoryCache(
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
}