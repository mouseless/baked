using Do.Architecture;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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
    }
}