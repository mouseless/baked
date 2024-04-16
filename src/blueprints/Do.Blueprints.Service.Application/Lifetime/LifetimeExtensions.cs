using Do.Architecture;
using Do.Lifetime;

namespace Do;

public static class LifetimeExtensions
{
    public static void AddLifetimes(this List<IFeature> source, IEnumerable<Func<LifetimeConfigurator, IFeature<LifetimeConfigurator>>> configures) =>
        source.AddRange(configures.Select(configure => configure(new())));
}