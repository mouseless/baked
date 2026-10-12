using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;

namespace Baked;

public static class NameBasedLabelCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> NameBasedLabel(this CodingStyleFeature.Configurator x,
        IEnumerable<string>? propertyNames = default
    ) => x.Define(new HashSet<string>(propertyNames ?? ["Display", "Label", "Name", "Title"]), (configurator, propertyNames) =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetPropertyAttribute(
                when: c =>
                    c.Property.IsPublic &&
                    (
                        c.Property.PropertyType.Is<string>() ||
                        c.Property.PropertyType.TryGetMetadata(out var metadata) && metadata.Has<Primitive>()
                    ) &&
                    propertyNames.Contains(c.Property.Name),
                attribute: () => new Label(),
                order: Order.At.Infra + 10
            );
        });
    });
}