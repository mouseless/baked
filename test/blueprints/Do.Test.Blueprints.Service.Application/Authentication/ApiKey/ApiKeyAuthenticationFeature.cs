using Do.Architecture;
using Do.Authentication;
using Do.Test.Authentication.ApiKey;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace Do;

public class ApiKeyAuthenticationFeature(ApiKeyOptions _options) : IFeature<AuthenticationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAuthentication(configurations =>
        {
            configurations.Add(
                "ApiKey",
                options => options.AddScheme<AuthenticationHandler>("ApiKey", "ApiKey"),
                context => context.Request.Headers.ContainsKey("X-Api-Key".ToLowerInvariant()));
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(_options);
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.AddSecurityDefinition("ApiKey",
                new()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "X-Api-Key",
                    Description = $"Enter the api key",
                }
            );

            swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>("ApiKey");
        });
    }
}