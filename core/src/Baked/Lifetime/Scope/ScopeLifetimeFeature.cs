using Baked.Architecture;
using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class ScopeLifetimeFeature
{
    public static IFeature<LifetimeFeature.Configurator> Scope(this LifetimeFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Type.Add<Scoped>();
        });

        configurator.Domain.ConfigureDomainServiceCollection((services, domain) =>
        {
            foreach (var scoped in domain.Types.Having<Scoped>())
            {
                services.AddScoped(scoped, useFactory: true);
            }
        });
    });

    extension(DomainServiceCollection services)
    {
        public void AddScoped(TypeModel type,
            bool useFactory = true,
            bool forward = false
        ) => services.Add(type, ServiceLifetime.Scoped, useFactory: useFactory, forward: forward);
    }
}