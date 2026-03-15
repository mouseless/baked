using Baked.Architecture;

namespace Baked.Lifetime.Transient;

public class TransientLifetimeFeature : IFeature<LifetimeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<TransientAttribute>();
        });

        configurator.Domain.ConfigureDomainServiceCollection((services, domain) =>
        {
            foreach (var transient in domain.Types.Having<TransientAttribute>())
            {
                services.AddTransient(transient, useFactory: true);
            }
        });
    }
}