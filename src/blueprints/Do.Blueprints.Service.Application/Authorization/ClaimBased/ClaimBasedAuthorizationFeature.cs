using Do.Architecture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Authorization.ClaimBased;

public class ClaimBasedAuthorizationFeature(List<string> _claims, string? _baseClaim)
    : IFeature<AuthorizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddAuthorization(options =>
            {
                foreach (var claim in _claims)
                {
                    options.AddPolicy(claim, policy => policy.RequireClaim(claim));
                }
            });

            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationResultHandler>();
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseAuthorization(), order: 1);
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            if (_baseClaim is not null && _claims.Contains(_baseClaim))
            {
                conventions.Add(new AllActionsRequireBaseClaimConvention(_baseClaim));
                conventions.Add(new RequireNoClaimIsAllowAnonymousConvention());
            }

            conventions.Add(new RequireClaimIsAuthorizeWithClaimConvention());
        });
    }
}