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

    public static void AddSecurityRequirementToOperationsThatUse<TAttribute>(this SwaggerGenOptions swaggerGenOptions, string schemeId,
        string? documentName = default
    ) where TAttribute : Attribute =>
        swaggerGenOptions.OperationFilter<SecurityRequirementOperationFilter<TAttribute>>(schemeId, documentName ?? string.Empty);

    public static void AddParameterToOperationsThatUse<TAttribute>(this SwaggerGenOptions swaggerGenOptions, string name,
        ParameterLocation @in = ParameterLocation.Header,
        bool required = false,
        string? documentName = default
    ) where TAttribute : Attribute =>
        swaggerGenOptions.OperationFilter<AddParameterOperationFilter<TAttribute>>(name, @in, required, documentName ?? string.Empty);

    public static bool HasMetadata<T>(this HttpContext httpContext) where T : Attribute
    {
        var metadata = httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata;

        return metadata?.GetMetadata<T>() is not null;
    }
}