using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Test.Authentication;
using Baked.Test.ExceptionHandling;
using Baked.Test.Orm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace Baked.Test.ConfigurationOverrider;

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
            swaggerGenOptions.SwaggerDoc("samples", new() { Title = "Samples", Version = "v1" });
            swaggerGenOptions.SwaggerDoc("external", new() { Title = "External", Version = "v1" });

            swaggerGenOptions.AddSecurityDefinition("Custom",
                new()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "X-Custom-API-Key",
                    Description = "Demonstration of additional security definitions",
                },
                documentName: "external"
            );

            swaggerGenOptions.DocInclusionPredicate((documentName, api) =>
                documentName == "samples" ||
                documentName == "external" && api.GroupName == "ExternalSamples"
            );

            swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>("Custom", documentName: "external");
            swaggerGenOptions.AddParameterToOperationsThatUse<AuthorizeAttribute>("X-Security", @in: ParameterLocation.Header, documentName: "external");
        });

        configurator.ConfigureSwaggerUIOptions(swaggerUIOptions =>
        {
            swaggerUIOptions.SwaggerEndpoint($"samples/swagger.json", "Samples");
            swaggerUIOptions.SwaggerEndpoint($"external/swagger.json", "External");
        });
    }
}