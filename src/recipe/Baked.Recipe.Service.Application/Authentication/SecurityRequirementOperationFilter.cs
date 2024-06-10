using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Do.Authentication;

public class SecurityRequirementOperationFilter<T>(string _schemeId)
    : IOperationFilter where T : Attribute
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
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