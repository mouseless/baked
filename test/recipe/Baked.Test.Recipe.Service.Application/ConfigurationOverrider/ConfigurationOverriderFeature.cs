using Do.Architecture;
using Do.RestApi.Model;
using Do.Test.Authentication;
using Do.Test.ExceptionHandling;
using Do.Test.Orm;
using Microsoft.AspNetCore.Authorization;
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

        configurator.ConfigureApiModel(api =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            api.GetController<AuthenticationSamples>().Action[nameof(AuthenticationSamples.FormPostAuthenticate)].UseForm = true;
            api.GetController<ExceptionSamples>().Action[nameof(ExceptionSamples.Throw)].Parameter["handled"].From = ParameterModelFrom.Query;
            api.GetController<Entities>().AddSingleById<Entity>(domainModel);
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

            swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>("AdditionalSecurity");
            swaggerGenOptions.AddParameterToOperationsThatUse<AuthorizeAttribute>("X-Security", @in: ParameterLocation.Header);
        });
    }
}