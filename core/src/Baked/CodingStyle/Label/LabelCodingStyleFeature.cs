using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;

namespace Baked.CodingStyle.Label;

public class LabelCodingStyleFeature(IEnumerable<string> propertyNames)
    : IFeature<CodingStyleConfigurator>
{
    readonly HashSet<string> _propertyNames = [.. propertyNames];

    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetPropertyAttribute(
                when: c =>
                    (
                        c.Property.PropertyType.Is<string>() ||
                        c.Property.PropertyType.TryGetMetadata(out var metadata) && metadata.Has<ValueTypeAttribute>()
                    ) &&
                    _propertyNames.Contains(c.Property.Name),
                attribute: () => new LabelAttribute(),
                order: Order.At.Infra + 10
            );
        });
    }
}