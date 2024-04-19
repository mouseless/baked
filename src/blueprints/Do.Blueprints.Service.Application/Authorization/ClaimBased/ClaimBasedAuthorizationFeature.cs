using Do.Architecture;
using Microsoft.AspNetCore.Authorization;

namespace Do.Authorization.ClaimBased;

public class ClaimBasedAuthorizationFeature : IFeature<AuthorizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<AuthorizationMiddleware>();
        });
    }
}