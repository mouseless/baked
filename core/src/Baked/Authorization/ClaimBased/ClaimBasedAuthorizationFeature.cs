using Baked.Architecture;
using Baked.Domain.Configuration;
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
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetMethodAttribute(
                when: c => !c.Method.Has<RequireUserAttribute>() && c.Type.Has<AllowAnonymousAttribute>(),
                attribute: c => c.Type.Get<AllowAnonymousAttribute>(),
                order: Order.At.Infra
            );
            conventions.SetMethodAttribute(
                when: c => !c.Method.Has<RequireUserAttribute>() && c.Type.Has<RequireUserAttribute>(),
                attribute: c => c.Type.Get<RequireUserAttribute>(),
                order: Order.At.Infra
            );

            conventions.Add(new AllowAnonymousIsAllowAnonymousConvention(), order: Order.At.Max);
            conventions.Add(new RequireUserIsAuthorizeConvention(), order: Order.At.Max);
            conventions.Add(new AddBaseClaimsAsAuthorizePolicyConvention(_baseClaims), order: Order.At.Max);
            conventions.Add(new AddRequireUserClaimsAsAuthorizePolicyConvention(), order: Order.At.Max);
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