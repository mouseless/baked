using Do.Architecture;
using Do.Authentication;
using Do.Authentication.FixedToken;
using Do.RestApi.Model;
using Do.Test.Authentication;
using Do.Test.ExceptionHandling;
using Do.Test.Orm;
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
            var domainModel = configurator.Context.GetDomainModel();

            apiModel.References.Add<Middleware>();

            apiModel.Controller[nameof(AuthenticationSamples)].Action[nameof(AuthenticationSamples.TokenAuthentication)].AdditionalAttributes.Add(typeof(UseAttribute<Middleware>).GetCSharpFriendlyFullName());
            apiModel.Controller[nameof(AuthenticationSamples)].Action[nameof(AuthenticationSamples.FormPostAuthentication)].AdditionalAttributes.Add(typeof(UseAttribute<Middleware>).GetCSharpFriendlyFullName());
            apiModel.Controller[nameof(AuthenticationSamples)].Action[nameof(AuthenticationSamples.FormPostAuthentication)].UseForm = true;
            apiModel.Controller[nameof(ExceptionSamples)].Action[nameof(ExceptionSamples.Throw)].Parameter["handled"].From = ParameterModelFrom.Query;

            apiModel.Controller[nameof(Entities)].AddSingleById<Entity>(domainModel);
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