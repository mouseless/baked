using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Do.Documentation.Default;

public class ObjectResponseDocumentationFilter : IDocumentFilter
{
    Dictionary<string, OperationType> _operationTypes = new()
    {
        { "POST", OperationType.Post },
        { "GET", OperationType.Get },
        { "PATCH", OperationType.Patch },
        { "PUT", OperationType.Put },
        { "HEAD", OperationType.Head },
        { "TRACE", OperationType.Trace },
        { "OPTIONS", OperationType.Options },
    };

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            if (apiDescription.ActionDescriptor is not ControllerActionDescriptor controllerActionDescriptor) { continue; }
            if (!IsTargetReturnType(controllerActionDescriptor.MethodInfo.ReturnType)) { continue; }
            if (apiDescription.HttpMethod is null) { continue; }
            if (apiDescription.HttpMethod is null) { continue; }

            var path = $"/{apiDescription.RelativePath?.TrimEnd('/')}";
            var operation = swaggerDoc.Paths[path].Operations[_operationTypes[apiDescription.HttpMethod]];

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

    bool IsTargetReturnType(Type target) =>
        target == typeof(object) ||
        target == typeof(Task<object>);
}