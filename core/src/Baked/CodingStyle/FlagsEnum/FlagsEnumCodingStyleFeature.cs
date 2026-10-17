using Baked.Architecture;
using Baked.CodingStyle.FlagsEnum;

namespace Baked;

public static class FlagsEnumCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> FlagsEnum(this CodingStyleFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.DataAccess.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(new FlagsEnumTypeConvention());
        });

        configurator.RestApi.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.Converters.Insert(0, new FlagsEnumJsonConverter());
        });
    });
}