using Humanizer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.CodingStyle.ObjectAsJson;

public class ObjectResponseDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            if (apiDescription.ActionDescriptor is not ControllerActionDescriptor controllerActionDescriptor) { continue; }
            if (!IsObject(controllerActionDescriptor.MethodInfo.ReturnType)) { continue; }
            if (apiDescription.HttpMethod is null) { continue; }

            var path = $"/{apiDescription.RelativePath?.TrimEnd('/')}";
            var operation = swaggerDoc.Paths[path].Operations[Enum.Parse<OperationType>(apiDescription.HttpMethod.ToLowerInvariant().Pascalize())];
            if (!operation.Responses.Any())
            {
                operation.Responses.Add("200", new() { Description = "Success" });
            }

            operation.Responses["200"].Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new() { Schema = context.SchemaGenerator.GenerateSchema(typeof(object), new()) }
            };
        }
    }

    bool IsObject(Type target) =>
        target == typeof(object) ||
        target == typeof(Task<object>);
}