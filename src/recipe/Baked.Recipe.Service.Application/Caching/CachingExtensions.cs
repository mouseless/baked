using Baked.Architecture;
using Baked.Caching;

namespace Baked;

public static class CachingExtensions
{
    public static void AddCachings(this List<IFeature> features, IEnumerable<Func<CachingConfigurator, IFeature<CachingConfigurator>>> configures) =>
        features.AddRange(configures.Select(configure => configure(new())));
}