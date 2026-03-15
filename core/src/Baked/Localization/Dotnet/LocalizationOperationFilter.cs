using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Localization.Dotnet;

public class LocalizationOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var returnType = context.MethodInfo.ReturnType;
        if (returnType == typeof(RedirectResult) || returnType == typeof(Task<RedirectResult>)) { return; }

        operation.Parameters ??= new List<IOpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept-Language",
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Default = null
            }
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Culture",
            In = ParameterLocation.Query,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Default = null
            }
        });
    }
}