using Baked.Architecture;

namespace Baked.CodingStyle.FlagsEnum;

public class FlagsEnumCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.DataAccess.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(new FlagsEnumTypeConvention());
        });

        configurator.RestApi.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.Converters.Insert(0, new FlagsEnumJsonConverter());
        });
    }
}