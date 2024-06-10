using Do.Caching;
using Do.Caching.ScopedMemory;
using Do.Testing;
using Microsoft.Extensions.Caching.Memory;
using Shouldly;

namespace Do;

public static class ScopedMemoryCachingExtensions
{
    public static ScopedMemoryCachingFeature ScopedMemory(this CachingConfigurator _) =>
        new();

    public static IMemoryCache AMemoryCache(this Stubber giveMe,
        bool clear = false
    )
    {
        var getMemoryCache = giveMe.The<Func<IMemoryCache>>();
        var memoryCache = getMemoryCache();

        if (clear)
        {
            (memoryCache as MemoryCache)?.Clear();
        }

        return memoryCache;
    }

    public static void ShouldHaveCount(this IMemoryCache memoryCache, int count) =>
        ((MemoryCache)memoryCache).Count.ShouldBe(count);
}