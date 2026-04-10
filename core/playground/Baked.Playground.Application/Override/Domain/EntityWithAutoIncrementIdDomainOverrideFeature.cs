using Baked.Architecture;
using Baked.Business;
using Baked.Playground.CodingStyle.Id;

namespace Baked.Playground.Override.Domain;

public class EntityWithAutoIncrementIdDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddPropertyAttributeConfiguration<IdAttribute>(
                when: c => c.Type.Is<EntityWithAutoIncrementId>(),
                attribute: id => id.AutoIncrement()
            );
        });
    }
}