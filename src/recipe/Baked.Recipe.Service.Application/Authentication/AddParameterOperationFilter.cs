using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Authentication;

public class AddParameterOperationFilter<T>(OpenApiParameter _parameter, int _position, string _documentName)
    : IOperationFilter where T : Attribute
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!string.IsNullOrWhiteSpace(_documentName) && context.DocumentName != _documentName) { return; }
        if (!context.MethodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(T))) { return; }

        if (_position >= 0 && _position < operation.Parameters.Count)
        {
            operation.Parameters.Insert(_position, _parameter);
        }
        else
        {
            operation.Parameters.Add(_parameter);
        }
    }
}