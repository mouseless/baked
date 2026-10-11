using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.Lifetime.Scope;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class ScopeLifetimeExtensions
{
    extension(LifetimeConfigurator _)
    {
        public ScopeLifetimeFeature Scope() =>
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