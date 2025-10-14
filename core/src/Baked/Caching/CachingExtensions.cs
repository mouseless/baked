using Baked.Architecture;
using Baked.Caching;
using Microsoft.Extensions.Caching.Memory;
using Shouldly;

namespace Baked;

public static class CachingExtensions
{
    public static void AddCachings(this List<IFeature> features, IEnumerable<Func<CachingConfigurator, IFeature<CachingConfigurator>>> configures) =>
        features.AddRange(configures.Select(configure => configure(new())));

    public static void ShouldHaveCount(this IMemoryCache memoryCache, int count) =>
        ((MemoryCache)memoryCache).Count.ShouldBe(count);
}