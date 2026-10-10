using Baked.Architecture;

namespace Baked.Lifetime.Application;

public class ApplicationLifetimeFeature : IFeature<LifetimeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
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
    }
}