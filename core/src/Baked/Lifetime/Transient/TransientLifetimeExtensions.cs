using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.Lifetime.Transient;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class TransientLifetimeExtensions
{
    extension(LifetimeConfigurator _)
    {
        public TransientLifetimeFeature Transient() =>
            new();
    }

    extension(DomainServiceCollection services)
    {
        public void AddTransient(TypeModel type,
            bool useFactory = true,
            bool forward = false
        ) => services.Add(type, ServiceLifetime.Transient, useFactory: useFactory, forward: forward);
    }
}