using Baked.Architecture;
using Baked.Business;
using Baked.Theme.Default;
using Baked.Ui;

namespace Baked.Ux.LabelsAreFrozen;

public class LabelsAreFrozenUxFeature()
    : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                when: c => c.Property.Has<LabelAttribute>(),
                attribute: data => data.Order = -10
            );
            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                when: c => c.Property.Has<LabelAttribute>(),
                schema: dtc =>
                {
                    dtc.Frozen = true;
                    dtc.MinWidth = true;
                }
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                component: (dt, c) =>
                {
                    if (dt.Schema.DataKey is not null) { return; }

                    dt.Schema.DataKey = dt.Schema.Columns.FirstOrDefault(dtc => dtc.Frozen == true)?.Key;
                }
            );
        });
    }
}