using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Do.Authentication;

public class AddHeaderOperationFilter<T>(string[] _headers)
    : IOperationFilter where T : Attribute
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.MethodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(T))) { return; }

        foreach (var header in _headers)
        {
            operation.Parameters.Add(new()
            {
                Required = true,
                In = ParameterLocation.Header,
                Name = header
            });
        }
    }
}
