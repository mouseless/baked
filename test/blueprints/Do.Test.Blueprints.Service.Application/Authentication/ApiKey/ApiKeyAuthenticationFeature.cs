using Do.Architecture;
using Do.Authentication;
using Microsoft.AspNetCore.Authentication;

namespace Do.Test.Authentication.ApiKey;

public class ApiKeyAuthenticationFeature()
    : IFeature<AuthenticationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAuthenticationSchemeCollection(configuration =>
        {
            configuration.Add(
                name: "ApiKey",
                useBuilder: builder => builder.AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>(
                    "ApiKey",
                    opt => { }
                ),
                handles: context => context.Request.Headers.ContainsKey("X-Api-Key")
            );
        });
    }
}