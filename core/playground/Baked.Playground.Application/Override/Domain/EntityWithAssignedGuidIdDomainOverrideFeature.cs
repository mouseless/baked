using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.CodingStyle.TypeBasedId;

namespace Baked.Playground.Override.Domain;

public class EntityWithAssignedGuidIdDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditPropertyAttribute<IdProperty>(
                when: c => c.Type.Is<EntityWithAssignedGuidId>(),
                attribute: id => id.AssignedGuid(),
                order: Order.At.Override
            );
        });
    }
}