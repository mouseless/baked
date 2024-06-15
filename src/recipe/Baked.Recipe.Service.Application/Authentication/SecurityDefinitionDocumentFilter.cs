using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Authentication;

public class SecurityDefinitionDocumentFilter(string _schemeId, OpenApiSecurityScheme _scheme, string _documentName)
    : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (!string.IsNullOrWhiteSpace(_documentName) && context.DocumentName != _documentName) { return; }

        swaggerDoc.Components.SecuritySchemes[_schemeId] = _scheme;
    }
}