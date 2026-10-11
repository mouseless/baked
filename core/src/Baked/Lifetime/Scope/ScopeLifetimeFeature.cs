using Baked.Architecture;

namespace Baked.Lifetime.Scope;

public class ScopeLifetimeFeature : IFeature<LifetimeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
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
    }
}