using Baked.Architecture;
using Baked.Ui;

using B = Baked.Ui.Components;

namespace Baked.Ux.NumericValuesAreFormatted;

public class NumericValuesAreFormattedUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: dtc => dtc.AlignRight = true,
                when: c =>
                    c.Property.PropertyType.SkipNullable().Is<int>() ||
                    c.Property.PropertyType.SkipNullable().Is<double>() ||
                    c.Property.PropertyType.SkipNullable().Is<decimal>()
            );
            builder.Conventions.AddPropertyComponent(
                component: () => B.Number(),
                when: c => c.Property.PropertyType.SkipNullable().Is<int>()
            );
            builder.Conventions.AddPropertyComponent(
                component: () => B.Money(),
                when: c => c.Property.PropertyType.SkipNullable().Is<decimal>()
            );
            builder.Conventions.AddPropertyComponent(
                component: () => B.Rate(),
                when: c => c.Property.PropertyType.SkipNullable().Is<double>()
            );
        });
    }
}