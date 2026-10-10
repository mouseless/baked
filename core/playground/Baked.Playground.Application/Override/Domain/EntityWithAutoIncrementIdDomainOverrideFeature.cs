using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.CodingStyle.TypeBasedId;

namespace Baked.Playground.Override.Domain;

public class EntityWithAutoIncrementIdDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditPropertyAttribute<IdProperty>(
                when: c => c.Type.Is<EntityWithAutoIncrementId>(),
                attribute: id => id.AutoIncrement(),
                order: Order.At.Override
            );
        });
    }
}