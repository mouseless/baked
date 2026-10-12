using Baked.Architecture;
using Baked.Playground.Authentication.ApiKey;
using Microsoft.AspNetCore.Authentication;

namespace Baked;

public static class ApiKeyAuthenticationFeature
{
    public static IFeature<AuthenticationFeature.Configurator> ApiKey(this AuthenticationFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.HttpServer.ConfigureAuthenticationCollection(authentications =>
        {
            authentications.Add(
                scheme: "ApiKey",
                useBuilder: builder => builder.AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>(
                    "ApiKey",
                    opt => { }
                ),
                handles: context => context.Request.Headers.ContainsKey("X-Api-Key")
            );
        });
    });
}