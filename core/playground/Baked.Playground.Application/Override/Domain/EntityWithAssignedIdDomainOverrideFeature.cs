using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.CodingStyle.TypeBasedId;

namespace Baked.Playground.Override.Domain;

public class EntityWithAssignedIdDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditPropertyAttribute<IdProperty>(
                when: c => c.Type.Is<EntityWithAssignedId>(),
                attribute: id => id.Assigned(),
                order: Order.At.Override
            );
        });
    }
}