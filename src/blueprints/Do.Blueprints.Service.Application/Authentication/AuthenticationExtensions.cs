using Do.Architecture;
using Do.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;

namespace Do;

public static class AuthenticationExtensions
{
    public static void AddAuthentications(this List<IFeature> source, IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>> configures) =>
        source.AddRange(configures.Select(configure => configure(new())));

    public static void AddSecurityRequirementToOperationsThatUse<TAttribute>(this SwaggerGenOptions source, string schemeId) where TAttribute : Attribute =>
        source.OperationFilter<SecurityRequirementOperationFilter<TAttribute>>([schemeId]);

    public static void AddParameterToOperationsThatUse<TAttribute>(this SwaggerGenOptions source, string name,
        ParameterLocation @in = ParameterLocation.Header,
        bool required = false
    ) where TAttribute : Attribute =>
        source.OperationFilter<AddParameterOperationFilter<TAttribute>>(name, @in, required);

    public static bool HasMetadata<T>(this HttpContext source) where T : Attribute
    {
        var metadata = source.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata;

        return metadata?.GetMetadata<T>() is not null;
    }

    public static bool ShouldBeAuthenticatedResult(this AuthenticateResult result,
        IEnumerable<Claim>? claims = default,
        bool ensureClaimValue = false
    )
    {
        if (result.Failure is not null) { return false; }
        if (result.Principal is null) { return false; }
        if (!result.Succeeded) { return false; }

        if (claims is not null)
        {
            foreach (var claim in claims)
            {
                if (
                    result.Principal.Claims.FirstOrDefault(c =>
                        c.Type == claim.Type &&
                        (!ensureClaimValue || c.Value == claim.Value)
                    ) is null
                )
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool ShouldBeFailedResult(this AuthenticateResult result) =>
        result.Failure is not null && !result.Succeeded;
}