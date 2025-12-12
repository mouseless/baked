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
                when: c =>
                    c.Property.PropertyType.SkipNullable().Is<int>() ||
                    c.Property.PropertyType.SkipNullable().Is<double>() ||
                    c.Property.PropertyType.SkipNullable().Is<decimal>(),
                schema: dtc => dtc.AlignRight = true
            );
            builder.Conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<int>(),
                component: (c) => B.Number()
            );
            builder.Conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<decimal>(),
                component: () => B.Money()
            );
            builder.Conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<double>(),
                component: () => B.Rate()
            );
        });
    }
}