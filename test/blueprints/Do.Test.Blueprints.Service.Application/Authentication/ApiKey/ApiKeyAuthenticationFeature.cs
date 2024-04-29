using Do.Architecture;
using Do.Authentication;
using Do.Test.Authentication.ApiKey;

namespace Do;

public class ApiKeyAuthenticationFeature : IFeature<AuthenticationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAuthentication(configuration =>
        {
            configuration.AddScheme(
                "ApiKey",
                context => context.Request.Headers.ContainsKey("X-Api-Key".ToLowerInvariant()),
                configure: options => options.AddScheme<AuthenticationHandler>("ApiKey", "ApiKey")
            );
        });
    }
}