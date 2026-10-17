using Baked.Architecture;
using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class ApplicationLifetimeFeature
{
    public static IFeature<LifetimeFeature.Configurator> Application(this LifetimeFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Type.Add<Singleton>();
        });

        configurator.Domain.ConfigureDomainServiceCollection((services, domain) =>
        {
            foreach (var singleton in domain.Types.Having<Singleton>())
            {
                services.AddSingleton(singleton, forward: true);
            }
        });
    });

    extension(DomainServiceCollection services)
    {
        public void AddSingleton(TypeModel type,
            bool forward = false
        ) => services.Add(type, ServiceLifetime.Singleton, forward: forward);
    }
}