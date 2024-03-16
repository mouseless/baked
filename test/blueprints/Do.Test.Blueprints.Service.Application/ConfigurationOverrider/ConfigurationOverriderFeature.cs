using Do.Architecture;
using Do.Authentication.FixedToken;
using Microsoft.OpenApi.Models;

namespace Do.Test.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTypeCollection(types => types.Add<OperationWithGenericParameter<Entity>>());

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Override<Entity>(x => x.Map(e => e.String).Length(500));
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.AddSecurityDefinition("AdditionalSecurity",
                new()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "X-Secret",
                    Description = "Enter secret information",
                }
            );

            swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<Middleware>("AdditionalSecurity");
            swaggerGenOptions.AddParameterToOperationsThatUse<Middleware>("X-Security", @in: ParameterLocation.Header, required: true);
        });

        configurator.ConfigureCodeCollection(codes =>
        {
            codes.Add(Entities.Code);
            codes.Add(Parents.Code);
            codes.Add(Remote.Code);
            codes.Add(Singleton.Code);
        });

        configurator.ConfigureApplicationParts(applicationParts =>
        {
            applicationParts.Add(new(configurator.Context.GetGeneratedAssembly().Assembly));
        });
    }
}
