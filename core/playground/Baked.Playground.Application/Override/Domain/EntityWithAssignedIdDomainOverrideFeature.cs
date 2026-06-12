using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.CodingStyle.Id;

namespace Baked.Playground.Override.Domain;

public class EntityWithAssignedIdDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddPropertyAttributeConfiguration<IdAttribute>(
                when: c => c.Type.Is<EntityWithAssignedId>(),
                attribute: id => id.Assigned(),
                order: Order.At.Override
            );
        });
    }
}