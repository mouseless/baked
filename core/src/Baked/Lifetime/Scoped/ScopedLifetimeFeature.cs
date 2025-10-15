using Baked.Architecture;

namespace Baked.Lifetime.Scoped;

public class ScopedLifetimeFeature : IFeature<LifetimeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ScopedAttribute>();
        });

        configurator.ConfigureDomainServiceCollection((services, domain) =>
        {
            foreach (var scoped in domain.Types.Having<ScopedAttribute>())
            {
                services.AddScoped(scoped, useFactory: true);
            }
        });
    }
}