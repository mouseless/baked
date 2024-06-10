using Do.Lifetime;
using Do.Lifetime.Scoped;

namespace Do;

public static class ScopedLifetimeExtensions
{
    public static ScopedLifetimeFeature Scoped(this LifetimeConfigurator _) =>
        new();
}