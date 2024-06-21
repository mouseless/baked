using Baked.Architecture;
using Baked.Caching;

namespace Baked;

public static class CachingExtensions
{
    public static void AddCaching(this List<IFeature> features, Func<CachingConfigurator, IFeature<CachingConfigurator>> configure) =>
        features.Add(configure(new()));
}