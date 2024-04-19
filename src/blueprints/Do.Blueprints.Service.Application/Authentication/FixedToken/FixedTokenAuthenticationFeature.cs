using Do.Architecture;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Do.Authentication.FixedToken;

public class FixedTokenAuthenticationFeature(List<string> _tokenNames)
    : IFeature<AuthenticationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, FixedBearerTokenAuthenticationHandler>(
                    "FixedBearerToken",
                    opts => { }
                );
            services.AddSingleton(new FixedBearerTokenOptions { TokenNames = _tokenNames });

        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseAuthentication(),
                order: 100
            );
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.AddSecurityDefinition("FixedToken",
                new()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = $"Enter the {string.Join(" or ", _tokenNames).ToLowerInvariant()} token",
                }
            );

            swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>("FixedToken");
        });
    }
}