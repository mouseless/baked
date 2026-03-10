using Baked.Architecture;
using Baked.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked;

public static class AuthenticationExtensions
{
    extension(List<IFeature> features)
    {
        public void AddAuthentications(IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>> configures) =>
            features.AddRange(configures.Select(configure => configure(new())));
    }

    extension(SwaggerGenOptions swaggerGenOptions)
    {
        public void AddSecurityDefinition(string schemeId, OpenApiSecurityScheme scheme, string? documentName) =>
            swaggerGenOptions.DocumentFilter<SecurityDefinitionDocumentFilter>(schemeId, scheme, documentName ?? string.Empty);

        public void AddSecurityRequirementToOperationsThatUse<TAttribute>(IEnumerable<string> schemeIds,
            bool includeRedirects = false,
            string? documentName = default
        ) where TAttribute : Attribute =>
            swaggerGenOptions.OperationFilter<SecurityRequirementOperationFilter<TAttribute>>(schemeIds, includeRedirects, documentName ?? string.Empty);

        public void AddParameterToOperationsThatUse<TAttribute>(OpenApiParameter parameter,
            int position = -1,
            string? documentName = default
        ) where TAttribute : Attribute =>
            swaggerGenOptions.OperationFilter<AddParameterOperationFilter<TAttribute>>(parameter, position, documentName ?? string.Empty);

        public void AddFormParameterToRedirectOperationsThatUse<TAttribute>(string name, OpenApiSchema property,
            string? documentName = default
        ) where TAttribute : Attribute =>
            swaggerGenOptions.OperationFilter<AddFormParameterToRedirectOperationFilter<TAttribute>>(name, property, documentName ?? string.Empty);
    }

    extension(HttpContext httpContext)
    {
        public bool HasMetadata<T>() where T : Attribute
        {
            var metadata = httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata;

            return metadata?.GetMetadata<T>() is not null;
        }
    }
}