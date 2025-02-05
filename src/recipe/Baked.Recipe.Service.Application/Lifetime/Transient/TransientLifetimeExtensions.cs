using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.Lifetime.Transient;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class TransientLifetimeExtensions
{
    public static TransientLifetimeFeature Transient(this LifetimeConfigurator _) =>
        new();

    public static void AddTransient(this DomainServiceCollection services, TypeModel type,
        bool useFactory = true,
        bool forward = false
    ) => services.Add(type, ServiceLifetime.Transient, useFactory: useFactory, forward: forward);
}