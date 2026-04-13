using Baked.Architecture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Authorization.ClaimBased;

public class ClaimBasedAuthorizationFeature(IEnumerable<string> _claims, IEnumerable<string> _baseClaims)
    : IFeature<AuthorizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetMethodAttribute(
                attribute: c => c.Type.Get<AllowAnonymousAttribute>(),
                when: c => !c.Method.Has<RequireUserAttribute>() && c.Type.Has<AllowAnonymousAttribute>()
            );
            builder.Conventions.SetMethodAttribute(
                attribute: c => c.Type.Get<RequireUserAttribute>(),
                when: c => !c.Method.Has<RequireUserAttribute>() && c.Type.Has<RequireUserAttribute>()
            );

            builder.Conventions.Add(new AllowAnonymousIsAllowAnonymousConvention());
            builder.Conventions.Add(new RequireUserIsAuthorizeConvention());
            builder.Conventions.Add(new AddBaseClaimsAsAuthorizePolicyConvention(_baseClaims));
            builder.Conventions.Add(new AddRequireUserClaimsAsAuthorizePolicyConvention());
        });

        configurator.Domain.ConfigureAttributeExportCollection(exports =>
        {
            exports.RestApi(restApi =>
            {
                restApi.Include<RequireUserAttribute>()
                    .AddPropertyFilter(_ => false);
                restApi.Include<AllowAnonymousAttribute>();
            });
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
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

        configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseAuthorization(), order: 20);
        });

        configurator.RestApi.ConfigureApiModel(api =>
        {
            api.Usings.Add("Microsoft.AspNetCore.Authorization");
        });
    }
}