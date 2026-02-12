using Baked.Architecture;
using Baked.Business;
using Baked.Ui;

namespace Baked.Ux.LabelsAreFrozen;

public class LabelsAreFrozenUxFeature()
    : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: dtc =>
                {
                    dtc.Frozen = true;
                    dtc.MinWidth = true;
                },
                when: c => c.Property.Has<LabelAttribute>()
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