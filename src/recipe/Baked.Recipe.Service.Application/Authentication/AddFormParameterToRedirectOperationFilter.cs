using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Authentication;

public class AddFormParameterToRedirectOperationFilter<T>(string _name, OpenApiSchema _property, string _documentName)
    : IOperationFilter where T : Attribute
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!string.IsNullOrWhiteSpace(_documentName) && context.DocumentName != _documentName) { return; }
        if (!context.MethodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(T))) { return; }
        if (operation.RequestBody is null) { return; }
        if (operation.RequestBody.Content is null) { return; }
        if (!operation.RequestBody.Content.TryGetValue("multipart/form-data", out var content)) { return; }
        if (operation.Responses is null || !operation.Responses.ContainsKey("302")) { return; }

        content.Schema.Required.Add(_name);
        content.Schema.Properties[_name] = _property;
    }
}