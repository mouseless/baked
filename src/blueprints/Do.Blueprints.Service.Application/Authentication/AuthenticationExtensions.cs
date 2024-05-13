using Do.Architecture;
using Do.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Shouldly;
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

    public static void ShouldBeSuccededResult(this AuthenticateResult result, IEnumerable<string> claims) =>
        ShouldBeSuccededResult(result, claims.Select(c => new Claim(c, c)), true);

    public static void ShouldBeSuccededResult(this AuthenticateResult result,
        IEnumerable<Claim>? claims = default,
        bool ensureClaimValue = false
    )
    {
        result.Failure.ShouldBeNull();
        result.Succeeded.ShouldBeTrue();
        result.Principal.ShouldNotBeNull();
        result.Principal.Identity.ShouldNotBeNull();
        result.Principal.Identity.IsAuthenticated.ShouldBeTrue();

        if (claims is not null)
        {
            result.Principal.Claims.ShouldContain(c => claims.Any(e => e.Type == c.Type && (!ensureClaimValue || e.Value == c.Value)));
        }
    }

    public static void ShouldBeNoResult(this AuthenticateResult result)
    {
        result.Failure.ShouldBeNull();
        result.Succeeded.ShouldBeFalse();
        result.Principal?.Claims.Any().ShouldBeFalse();
    }

    public static void ShouldBeFailedResult(this AuthenticateResult result)
    {
        result.Failure.ShouldNotBeNull();
        result.Failure.ShouldBeAssignableTo<AuthenticationFailureException>();
        result.Succeeded.ShouldBeFalse();
    }
}