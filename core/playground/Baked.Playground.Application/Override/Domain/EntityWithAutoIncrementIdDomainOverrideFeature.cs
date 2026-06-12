using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.CodingStyle.Id;

namespace Baked.Playground.Override.Domain;

public class EntityWithAutoIncrementIdDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddPropertyAttributeConfiguration<IdAttribute>(
                when: c => c.Type.Is<EntityWithAutoIncrementId>(),
                attribute: id => id.AutoIncrement(),
                order: Order.At.Override
            );
        });
    }
}