using Baked.Architecture;
using Baked.Business;
using Baked.Playground.CodingStyle.Id;

namespace Baked.Playground.Override.Domain;

public class EntityWithAssignedGuidIdDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddPropertyAttributeConfiguration<IdAttribute>(
                when: c => c.Type.Is<EntityWithAssignedGuidId>(),
                attribute: id => id.AssignedGuid()
            );
        });
    }
}