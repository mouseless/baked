using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.CodingStyle.Locatable;

namespace Baked.Playground.Override.Domain;

public class ILocatableDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c => c.Type.Is<ILocatable>(),
                attribute: () => new LocatableAttribute(),
                order: Order.At.Override
            );
        });
    }
}