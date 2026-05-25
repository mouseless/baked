using Baked.Architecture;
using Baked.Ui;

using B = Baked.Ui.Components;

namespace Baked.Ux.NumericValuesAreFormatted;

public class NumericValuesAreFormattedUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                when: c =>
                    c.Property.PropertyType.SkipNullable().Is<int>() ||
                    c.Property.PropertyType.SkipNullable().Is<long>() ||
                    c.Property.PropertyType.SkipNullable().Is<double>() ||
                    c.Property.PropertyType.SkipNullable().Is<decimal>(),
                schema: dtc => dtc.AlignRight = true
            );
            conventions.AddPropertyComponent(
                when: c =>
                    c.Property.PropertyType.SkipNullable().Is<int>() ||
                    c.Property.PropertyType.SkipNullable().Is<long>(),
                component: (c) => B.Number()
            );
            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<decimal>(),
                component: () => B.Money()
            );
            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().Is<double>(),
                component: () => B.Rate()
            );
        });
    }
}