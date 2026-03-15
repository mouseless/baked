using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.CodingStyle.ObjectAsJson;

public class ObjectResponseOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!IsObject(context.MethodInfo.ReturnType)) { return; }

        operation.Responses ??= new OpenApiResponses();

        OpenApiResponse response;
        if (operation.Responses.TryGetValue("200", out var existingResponse) && existingResponse is OpenApiResponse concreteResponse)
        {
            response = concreteResponse;
        }
        else
        {
            operation.Responses["200"] = response = new OpenApiResponse() { Description = "Success" };
        }

        if (response.Content is null)
        {
            response.Content = new Dictionary<string, OpenApiMediaType>();
        }

        if (!response.Content.TryGetValue("application/json", out var mediaType))
        {
            response.Content["application/json"] = mediaType = new() { };
        }

        mediaType.Schema = context.SchemaGenerator.GenerateSchema(typeof(object), new());
    }

    bool IsObject(Type target) =>
        target == typeof(object) ||
        target == typeof(Task<object>);
}