using Do.Architecture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Authorization.ClaimBased;

public class ClaimBasedAuthorizationFeature(IEnumerable<string> _claims, IEnumerable<string> _baseClaims)
    : IFeature<AuthorizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddMethodMetadata(
                apply: (c, add) => add(c.Method, c.Type.GetSingle<AllowAnonymousAttribute>()),
                when: c => !c.Method.Has<RequireUserAttribute>() && c.Type.Has<AllowAnonymousAttribute>()
            );
            builder.Conventions.AddMethodMetadata(
                apply: (c, add) => add(c.Method, c.Type.GetSingle<RequireUserAttribute>()),
                when: c => !c.Method.Has<RequireUserAttribute>() && c.Type.Has<RequireUserAttribute>()
            );
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddAuthorization(options =>
            {
                foreach (var claim in _claims)
                {
                    options.AddPolicy(claim, policy => policy.RequireClaim(claim));
                }
            });

            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseAuthorization(), order: 10);
        });

        configurator.ConfigureApiModel(api =>
        {
            api.Usings.Add("Microsoft.AspNetCore.Authorization");
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new AllowAnonymousIsAllowAnonymousConvention());
            conventions.Add(new RequireUserIsAuthorizeConvention());
            conventions.Add(new AddBaseClaimsAsAuthorizePolicyConvention(_baseClaims));
            conventions.Add(new AddRequireUserClaimsAsAuthorizePolicyConvention());
        });
    }
}