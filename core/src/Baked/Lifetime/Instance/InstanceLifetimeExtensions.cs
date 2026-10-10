using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.Lifetime.Instance;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class InstanceLifetimeExtensions
{
    extension(LifetimeConfigurator _)
    {
        public InstanceLifetimeFeature Instance() =>
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