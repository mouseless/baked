using Do.Architecture;
using Do.Caching;

namespace Do;

public static class CachingExtensions
{
    public static void AddCaching(this List<IFeature> source, Func<CachingConfigurator, IFeature<CachingConfigurator>> configure) =>
        source.Add(configure(new()));
}