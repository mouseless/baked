using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.Lifetime.Scoped;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class ScopedLifetimeExtensions
{
    public static ScopedLifetimeFeature Scoped(this LifetimeConfigurator _) =>
        new();

    public static void AddScoped(this DomainServiceCollection services, TypeModel type,
        bool useFactory = true,
        bool forward = false
    ) => services.Add(type, ServiceLifetime.Scoped, useFactory: useFactory, forward: forward);
}