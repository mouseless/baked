using Do.Lifetime;
using Do.Lifetime.Transient;

namespace Do;

public static class TransientLifetimeExtensions
{
    public static TransientLifetimeFeature Transient(this LifetimeConfigurator _) =>
        new();
}