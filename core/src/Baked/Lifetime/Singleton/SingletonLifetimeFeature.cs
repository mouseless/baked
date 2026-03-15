using Baked.Architecture;

namespace Baked.Lifetime.Singleton;

public class SingletonLifetimeFeature : IFeature<LifetimeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<SingletonAttribute>();
        });

        configurator.Domain.ConfigureDomainServiceCollection((services, domain) =>
        {
            foreach (var singleton in domain.Types.Having<SingletonAttribute>())
            {
                services.AddSingleton(singleton, forward: true);
            }
        });
    }
}