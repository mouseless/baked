using Do;
using Do.Architecture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Do.Authentication.FixedBearerToken;

public class FixedBearerTokenAuthenticationFeature(List<Token> _tokens)
    : IFeature<AuthenticationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAuthenticationSchemeCollection(configuration =>
        {
            configuration.Add(
                name: "FixedBearerToken",
                handles: context => context.Request.Headers.Authorization.Any() || (context.Request.HasFormContentType && context.Request.Form.ContainsKey("hash")),
                configureOptions: options => options.AddScheme<AuthenticationHandler>("FixedBearerToken", "FixedBearerToken"));
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(new TokenOptions(_tokens));
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.AddSecurityDefinition("FixedBearerToken",
                new()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = $"Enter the {string.Join(" or ", _tokens.Select(t => t.Name)).ToLowerInvariant()} token",
                }
            );

            swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>("FixedBearerToken");
        });
    }
}