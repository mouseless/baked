using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.Lifetime.Scoped;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class ScopedLifetimeExtensions
{
    extension(LifetimeConfigurator _)
    {
        public ScopedLifetimeFeature Scoped() =>
            new();
    }

    extension(DomainServiceCollection services)
    {
        public void AddScoped(TypeModel type,
            bool useFactory = true,
            bool forward = false
        ) => services.Add(type, ServiceLifetime.Scoped, useFactory: useFactory, forward: forward);
    }
}