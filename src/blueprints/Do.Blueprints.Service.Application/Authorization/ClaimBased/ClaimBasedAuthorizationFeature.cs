using Do.Architecture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Authorization.ClaimBased;

public class ClaimBasedAuthorizationFeature(string? _baseClaim, List<string> _claims)
    : IFeature<AuthorizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddAuthorization(options =>
            {
                if (_baseClaim is not null)
                {
                    _claims.Add(_baseClaim);
                }

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
            if (_baseClaim is not null)
            {
                conventions.Add(new AllRequestsShouldRequireBaseClaimConvention(_baseClaim));
                conventions.Add(new RequireNoClaimIsAllowAnonymousAttributeConvention());
            }

            conventions.Add(new RequireClaimIsAuthorizeAttributeConvention());
        });
    }
}