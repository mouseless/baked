using Baked.Lifetime;
using Baked.Lifetime.Transient;

namespace Baked;

public static class TransientLifetimeExtensions
{
    public static TransientLifetimeFeature Transient(this LifetimeConfigurator _) =>
        new();
}