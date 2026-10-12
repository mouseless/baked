using Baked.Architecture;
using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class InstanceLifetimeFeature
{
    public static IFeature<LifetimeFeature.Configurator> Instance(this LifetimeFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Type.Add<Transient>();
        });

        configurator.Domain.ConfigureDomainServiceCollection((services, domain) =>
        {
            foreach (var transient in domain.Types.Having<Transient>())
            {
                services.AddTransient(transient, useFactory: true);
            }
        });
    });

    extension(DomainServiceCollection services)
    {
        public void AddTransient(TypeModel type,
            bool useFactory = true,
            bool forward = false
        ) => services.Add(type, ServiceLifetime.Transient, useFactory: useFactory, forward: forward);
    }
}