using Do.Architecture;
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
                .AddScheme<FixedBearerTokenOptions, FixedBearerTokenAuthenticationHandler>(
                    "FixedBearerToken",
                    opts =>
                    {
                        opts.TokenNames.AddRange(_tokenNames);
                    }
                );
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseAuthentication(),
                order: 100
            );
        });
    }
}