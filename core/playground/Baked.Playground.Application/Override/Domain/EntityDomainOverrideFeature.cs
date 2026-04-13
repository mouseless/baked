using Baked.Architecture;
using Baked.Playground.Orm;

namespace Baked.Playground.Override.Domain;

public class EntityDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddLocateAction<Entity>();
        });
    }
}