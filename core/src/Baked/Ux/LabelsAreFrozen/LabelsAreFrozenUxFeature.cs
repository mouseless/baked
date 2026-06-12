using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Theme.Default;
using Baked.Ui;

namespace Baked.Ux.LabelsAreFrozen;

public class LabelsAreFrozenUxFeature()
    : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                when: c => c.Property.Has<LabelAttribute>(),
                attribute: data => data.Order = -10,
                order: Order.At.Infra
            );
            conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                when: c => c.Property.Has<LabelAttribute>(),
                schema: dtc =>
                {
                    dtc.Frozen = true;
                    dtc.MinWidth = true;
                }
            );
            conventions.AddMethodComponentConfiguration<DataTable>(
                component: (dt, c) =>
                {
                    if (dt.Schema.DataKey is not null) { return; }

                    dt.Schema.DataKey = dt.Schema.Columns.FirstOrDefault(dtc => dtc.Frozen == true)?.Key;
                }
            );
        });
    }
}