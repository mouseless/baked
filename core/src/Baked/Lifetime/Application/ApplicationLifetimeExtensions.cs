using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.Lifetime.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class ApplicationLifetimeExtensions
{
    extension(LifetimeConfigurator _)
    {
        public ApplicationLifetimeFeature Application() =>
            new();
    }

    extension(DomainServiceCollection services)
    {
        public void AddSingleton(TypeModel type,
            bool forward = false
        ) => services.Add(type, ServiceLifetime.Singleton, forward: forward);
    }
}