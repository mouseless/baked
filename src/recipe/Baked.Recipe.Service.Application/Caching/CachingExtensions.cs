using Baked.Architecture;
using Baked.Caching;

namespace Baked;

public static class CachingExtensions
{
    public static void AddCaching(this List<IFeature> source, Func<CachingConfigurator, IFeature<CachingConfigurator>> configure) =>
        source.Add(configure(new()));
}