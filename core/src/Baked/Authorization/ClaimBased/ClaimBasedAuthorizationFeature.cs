using Baked.Architecture;
using Baked.RestApi;
using Baked.RestApi.Model;
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
                when: c => !c.Method.Has<RequireUserAttribute>() && c.Type.Has<AllowAnonymousAttribute>(),
                attribute: c => c.Type.Get<AllowAnonymousAttribute>()
            );
            builder.Conventions.SetMethodAttribute(
                when: c => !c.Method.Has<RequireUserAttribute>() && c.Type.Has<RequireUserAttribute>(),
                attribute: c => c.Type.Get<RequireUserAttribute>()
            );

            builder.Conventions.Add(new AllowAnonymousIsAllowAnonymousConvention(), order: RestApiLayer.MaxConventionOrder - 10);
            builder.Conventions.Add(new RequireUserIsAuthorizeConvention(), order: RestApiLayer.MaxConventionOrder - 10);
            builder.Conventions.Add(new AddBaseClaimsAsAuthorizePolicyConvention(_baseClaims), order: RestApiLayer.MaxConventionOrder - 10);
            builder.Conventions.Add(new AddRequireUserClaimsAsAuthorizePolicyConvention(), order: RestApiLayer.MaxConventionOrder - 10);
        });

        configurator.Domain.ConfigureExportConfigurations(exports =>
        {
            exports.Build("RestApi", export => export
                .Include<ActionModelAttribute>()
                .AddProperty(action => new("anonymous", Value: action.AdditionalAttributes.Any(a => a.Contains("AllowAnonymous"))))
                .AddProperty(action =>
                {
                    const string before = "Authorize(Policy = \"";
                    const string after = "\")";
                    var claims = action.AdditionalAttributes
                        .Where(a => a.StartsWith(before) && a.EndsWith(after))
                        .Select(a => a[before.Length..^after.Length])
                    ;

                    return new("required-claims", Value: claims.Any() ? claims.Join(", ") : null);
                })
            );
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