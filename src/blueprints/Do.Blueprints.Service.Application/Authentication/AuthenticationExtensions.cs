using Do.Architecture;
using Do.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Do;

public static class AuthenticationExtensions
{
    public static void AddAuthentication(this IList<IFeature> source, Func<AuthenticationConfigurator, IFeature> configure) => source.Add(configure(new()));

    public static void AddSecurityRequirementToOperationsThatUse<TMiddleware>(this SwaggerGenOptions source, string schemeId) => source.OperationFilter<SecurityRequirementOperationFilter<UseAttribute<TMiddleware>>>([schemeId]);
    public static void AddParameterToOperationsThatUse<TMiddleware>(this SwaggerGenOptions source, string name,
        ParameterLocation @in = ParameterLocation.Header,
        bool required = false
    ) => source.OperationFilter<AddParameterOperationFilter<UseAttribute<TMiddleware>>>(name, @in, required);

    public static bool HasMetadata<T>(this HttpContext source) where T : Attribute
    {
        var metadata = source.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata;

        return metadata?.GetMetadata<T>() is not null;
    }
}
