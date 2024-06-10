using Baked.Lifetime;
using Baked.Lifetime.Scoped;

namespace Baked;

public static class ScopedLifetimeExtensions
{
    public static ScopedLifetimeFeature Scoped(this LifetimeConfigurator _) =>
        new();
}