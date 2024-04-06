using Do.Lifetime;
using Do.Lifetime.Singleton;

namespace Do;

public static class SingletonLifetimeExtensions
{
    public static SingletonLifetimeFeature Singleton(this LifetimeConfigurator _) => new();
}