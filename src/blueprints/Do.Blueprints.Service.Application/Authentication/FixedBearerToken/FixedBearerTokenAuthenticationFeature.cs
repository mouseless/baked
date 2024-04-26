using Do;
using Do.Architecture;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Do.Authentication.FixedBearerToken;

public class FixedBearerTokenAuthenticationFeature(List<Token> _tokens)
    : IFeature<AuthenticationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>(
                    "FixedBearerToken",
                    opts => { }
                );

            services.AddSingleton(new TokenOptions(_tokens));
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseAuthentication());
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