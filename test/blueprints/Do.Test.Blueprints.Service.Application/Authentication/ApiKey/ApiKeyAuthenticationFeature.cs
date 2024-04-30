using Do.Architecture;
using Do.Authentication;

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
                handles: context => context.Request.Headers.ContainsKey("X-Api-Key"),
                configureOptions: options => options.AddScheme<AuthenticationHandler>("ApiKey", "ApiKey")
            );
        });
    }
}