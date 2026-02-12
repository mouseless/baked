using Baked.Architecture;
using Baked.Business;

namespace Baked.CodingStyle.Label;

public class LabelCodingStyleFeature(IEnumerable<string> propertyNames)
    : IFeature<CodingStyleConfigurator>
{
    readonly HashSet<string> _propertyNames = [.. propertyNames];

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetPropertyAttribute(
                attribute: () => new LabelAttribute(),
                when: c => c.Property.PropertyType.Is<string>() && _propertyNames.Contains(c.Property.Name)
            );
        });
    }
}