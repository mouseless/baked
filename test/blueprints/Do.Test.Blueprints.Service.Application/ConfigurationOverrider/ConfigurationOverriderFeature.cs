using Do.Architecture;
using Do.Authentication;
using Do.Authentication.FixedToken;
using Do.RestApi.Model;
using Microsoft.OpenApi.Models;

namespace Do.Test.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Override<Entity>(x => x.Map(e => e.String).Length(500));
            model.Override<Entity>(x => x.Map(e => e.Unique).Column("UniqueString").Unique());
        });

        configurator.ConfigureApiModel(apiModel =>
        {
            apiModel.References.Add<Middleware>();
            apiModel.Controller[nameof(Singleton)].Action[nameof(Singleton.GetTime)].AdditionalAttributes.Add(typeof(UseAttribute<Middleware>).GetCSharpFriendlyFullName());
            apiModel.Controller[nameof(Singleton)].Action[nameof(Singleton.TestFormPostAuthentication)].AdditionalAttributes.Add(typeof(UseAttribute<Middleware>).GetCSharpFriendlyFullName());
            apiModel.Controller[nameof(Singleton)].Action[nameof(Singleton.TestException)].Parameter["handled"].From = ParameterModelFrom.Query;
            apiModel.Controller[nameof(Singleton)].Action[nameof(Singleton.TestFormPostAuthentication)].UseForm = true;
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

    }
}
