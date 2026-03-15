using Baked.Architecture;
using Baked.Caching;
using Microsoft.Extensions.Caching.Memory;
using Shouldly;

namespace Baked;

public static class CachingExtensions
{
    extension(List<IFeature> features)
    {
        public void AddCachings(IEnumerable<FeatureFunc<CachingConfigurator>> configures) =>
            features.AddRange(configures.Select(configure => configure(new())));
    }

    extension(IMemoryCache memoryCache)
    {
        public void ShouldHaveCount(int count) =>
            ((MemoryCache)memoryCache).Count.ShouldBe(count);
    }
}