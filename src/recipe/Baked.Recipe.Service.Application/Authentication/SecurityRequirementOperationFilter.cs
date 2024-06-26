using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Authentication;

public class SecurityRequirementOperationFilter<T>(IEnumerable<string> _schemeIds, bool _includeRedirects, string _documentName)
    : IOperationFilter where T : Attribute
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!string.IsNullOrWhiteSpace(_documentName) && context.DocumentName != _documentName) { return; }
        if (!context.MethodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(T))) { return; }
        if (!_includeRedirects && operation.Responses is not null && operation.Responses.ContainsKey("302")) { return; }

        var requirement = new OpenApiSecurityRequirement();
        foreach (var schemeId in _schemeIds)
        {
            requirement.Add(new() { Reference = new() { Type = ReferenceType.SecurityScheme, Id = schemeId } }, Array.Empty<string>());
        }

        operation.Security.Add(requirement);
    }
}