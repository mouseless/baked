using Baked.Architecture;

namespace Baked.Lifetime.Instance;

public class InstanceLifetimeFeature : IFeature<LifetimeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
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
    }
}