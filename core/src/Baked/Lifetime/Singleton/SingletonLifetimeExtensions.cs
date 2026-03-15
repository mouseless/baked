using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.Lifetime.Singleton;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class SingletonLifetimeExtensions
{
    extension(LifetimeConfigurator _)
    {
        public SingletonLifetimeFeature Singleton() =>
            new();
    }

    extension(DomainServiceCollection services)
    {
        public void AddSingleton(TypeModel type,
            bool forward = false
        ) => services.Add(type, ServiceLifetime.Singleton, forward: forward);
    }
}