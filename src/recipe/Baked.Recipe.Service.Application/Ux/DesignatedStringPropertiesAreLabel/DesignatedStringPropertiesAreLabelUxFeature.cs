using Baked.Architecture;
using Baked.Theme.Admin;

namespace Baked.Ux.DesignatedStringPropertiesAreLabel;

public class DesignatedStringPropertiesAreLabelUxFeature(IEnumerable<string> propertyNames)
    : IFeature<UxConfigurator>
{
    readonly HashSet<string> _propertyNames = [.. propertyNames];

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetPropertyMetadata(
                attribute: new LabelAttribute(),
                when: c => c.Property.PropertyType.Is<string>() && _propertyNames.Contains(c.Property.Name)
            );
            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: dtc =>
                {
                    dtc.Frozen = true;
                    dtc.MinWidth = true;
                },
                whenProperty: c => c.Property.Has<LabelAttribute>()
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                component: (dt, c) =>
                {
                    if (dt.Schema.DataKey is not null) { return; }

                    dt.Schema.DataKey = dt.Schema.Columns.FirstOrDefault(dtc => dtc.Frozen == true)?.Prop;
                }
            );
        });
    }
}