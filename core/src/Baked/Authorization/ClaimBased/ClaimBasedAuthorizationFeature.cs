using Baked.Architecture;
using Baked.Authorization;
using Baked.Authorization.ClaimBased;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class ClaimBasedAuthorizationFeature
{
    public static IFeature<AuthorizationFeature.Configurator> ClaimBased(this AuthorizationFeature.Configurator x,
        IEnumerable<string>? claims = default,
        IEnumerable<string>? baseClaims = default
    ) => x.Define((
        claims ?? [],
        baseClaims ?? []
    ), (configurator, args) =>
    {
        var (claims, baseClaims) = args;

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetMethodAttribute(
                when: c => !c.Method.Has<RequireUser>() && c.Type.Has<AllowAnonymous>(),
                attribute: c => c.Type.Get<AllowAnonymous>(),
                order: Order.At.Infra
            );
            conventions.SetMethodAttribute(
                when: c => !c.Method.Has<RequireUser>() && c.Type.Has<RequireUser>(),
                attribute: c => c.Type.Get<RequireUser>(),
                order: Order.At.Infra
            );

            conventions.Add(new AllowAnonymousIsAllowAnonymousConvention(), order: Order.At.Max);
            conventions.Add(new RequireUserIsAuthorizeConvention(), order: Order.At.Max);
            conventions.Add(new AddBaseClaimsAsAuthorizePolicyConvention(baseClaims), order: Order.At.Max);
            conventions.Add(new AddRequireUserClaimsAsAuthorizePolicyConvention(), order: Order.At.Max);
        });

        configurator.Domain.ConfigureExportConfigurations(exports =>
        {
            exports.Build("RestApi", export => export
                .Include<ApiAction>()
                .AddProperty(action => new("anonymous", Value: action.AdditionalAttributes.Any(a => a.Contains("AllowAnonymous"))))
                .AddProperty(action =>
                {
                    const string before = "Authorize(Policy = \"";
                    const string after = "\")";
                    var requiredClaims = action.AdditionalAttributes
                        .Where(a => a.StartsWith(before) && a.EndsWith(after))
                        .Select(a => a[before.Length..^after.Length])
                    ;

                    return new("required-claims", Value: requiredClaims.Any() ? requiredClaims.Join(", ") : null);
                })
            );
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddAuthorization(options =>
            {
                foreach (var claim in claims)
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
    });
}