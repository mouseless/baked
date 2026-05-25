using Baked.Architecture;
using Baked.Business;
using Baked.Playground.CodingStyle.Locatable;

namespace Baked.Playground.Override.Domain;

public class ILocatableDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c => c.Type.Is<ILocatable>(),
                attribute: () => new LocatableAttribute()
            );
        });
    }
}