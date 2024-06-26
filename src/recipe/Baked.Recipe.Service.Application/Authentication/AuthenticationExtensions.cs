using Baked.Architecture;
using Baked.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked;

public static class AuthenticationExtensions
{
    public static void AddAuthentications(this List<IFeature> features, IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>> configures) =>
        features.AddRange(configures.Select(configure => configure(new())));

    public static void AddSecurityDefinition(this SwaggerGenOptions swaggerGenOptions, string schemeId, OpenApiSecurityScheme scheme, string? documentName) =>
        swaggerGenOptions.DocumentFilter<SecurityDefinitionDocumentFilter>(schemeId, scheme, documentName ?? string.Empty);

    public static void AddSecurityRequirementToOperationsThatUse<TAttribute>(this SwaggerGenOptions swaggerGenOptions, IEnumerable<string> schemeIds,
        string? documentName = default
    ) where TAttribute : Attribute =>
        swaggerGenOptions.OperationFilter<SecurityRequirementOperationFilter<TAttribute>>(schemeIds, documentName ?? string.Empty);

    public static void AddParameterToOperationsThatUse<TAttribute>(this SwaggerGenOptions swaggerGenOptions, OpenApiParameter parameter,
        int position = -1,
        string? documentName = default
    ) where TAttribute : Attribute =>
        swaggerGenOptions.OperationFilter<AddParameterOperationFilter<TAttribute>>(parameter, position, documentName ?? string.Empty);

    public static bool HasMetadata<T>(this HttpContext httpContext) where T : Attribute
    {
        var metadata = httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata;

        return metadata?.GetMetadata<T>() is not null;
    }
}