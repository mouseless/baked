using Baked.Lifetime;
using Baked.Lifetime.Singleton;

namespace Baked;

public static class SingletonLifetimeExtensions
{
    public static SingletonLifetimeFeature Singleton(this LifetimeConfigurator _) =>
        new();
}