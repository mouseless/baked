using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.Lifetime.Singleton;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class SingletonLifetimeExtensions
{
    public static SingletonLifetimeFeature Singleton(this LifetimeConfigurator _) =>
        new();

    public static void AddSingleton(this DomainServiceCollection services, TypeModel type,
        bool forward = false
    ) => services.Add(type, ServiceLifetime.Singleton, forward: forward);
}