using Baked.Architecture;
using Microsoft.Extensions.Caching.Memory;
using Shouldly;

namespace Baked;

public static class CachingFeature
{
    public static void AddCachings(this List<IFeature> features,
        IEnumerable<FeatureFunc<Configurator>> configures
    ) => features.AddRange(configures.Select(configure => configure(new())));

    public class Configurator : FeatureConfigurator<Configurator>;

    extension(IMemoryCache memoryCache)
    {
        public void ShouldHaveCount(int count) =>
            ((MemoryCache)memoryCache).Count.ShouldBe(count);
    }
}