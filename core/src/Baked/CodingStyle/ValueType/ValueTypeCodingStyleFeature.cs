using Baked.Architecture;

namespace Baked.CodingStyle.ValueType;

public class ValueTypeCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(new ValueTypeConvention());
        });

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            // options.SerializerSettings.Converters.Add(new ValueTypeJsonConverter());
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            // swaggerGenOptions.MapType<Business.Id>(() => new OpenApiSchema { Type = "string" });
        });
    }
}