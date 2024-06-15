using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Authentication;

public class SecurityRequirementOperationFilter<T>(string _schemeId, string _documentName)
    : IOperationFilter where T : Attribute
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!string.IsNullOrWhiteSpace(_documentName) && context.DocumentName != _documentName) { return; }
        if (!context.MethodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(T))) { return; }

        operation.Security.Add(new()
        {
            {
                new() { Reference = new() { Type = ReferenceType.SecurityScheme, Id = _schemeId } },
                Array.Empty<string>()
            }
        });
    }
}