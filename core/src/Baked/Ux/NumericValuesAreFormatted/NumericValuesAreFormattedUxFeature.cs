using Baked.Architecture;
using Baked.Ui;

namespace Baked;

public static class NumericValuesAreFormattedUxFeature
{
    public static IFeature<UxFeature.Configurator> NumericValuesAreFormatted(this UxFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditPropertySchema<DataTable.Column>(
                when: c =>
                    c.Property.PropertyType.SkipNullable().Is<int>() ||
                    c.Property.PropertyType.SkipNullable().Is<long>() ||
                    c.Property.PropertyType.SkipNullable().Is<double>() ||
                    c.Property.PropertyType.SkipNullable().Is<decimal>(),
                schema: dtc => dtc.AlignRight = true
            );
            conventions.AddPropertyComponent<Number>(
                when: c =>
                    c.Property.PropertyType.SkipNullable().Is<int>() ||
                    c.Property.PropertyType.SkipNullable().Is<long>()
            );
            conventions.AddPropertyComponent<Money>(
                when: c => c.Property.PropertyType.SkipNullable().Is<decimal>()
            );
            conventions.AddPropertyComponent<Rate>(
                when: c => c.Property.PropertyType.SkipNullable().Is<double>()
            );
        });
    });
}