using Baked.Architecture;
using Baked.Lifetime;

namespace Baked;

public static class LifetimeExtensions
{
    public static void AddLifetimes(this List<IFeature> features, IEnumerable<Func<LifetimeConfigurator, IFeature<LifetimeConfigurator>>> configures) =>
        features.AddRange(configures.Select(configure => configure(new())));
}