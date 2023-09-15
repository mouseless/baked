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
        { "Options", OperationType.Options },
    };

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            var controllerActionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                if (IsTargetReturnType(controllerActionDescriptor.MethodInfo.ReturnType))
                {
                    var path = "/" + apiDescription.RelativePath!.TrimEnd('/');
                    var operation = swaggerDoc.Paths[path].Operations[_operationTypes[apiDescription.HttpMethod!]];

                    if (!operation.Responses.Any())
                    {
                        operation.Responses.Add("200", new OpenApiResponse
                        {
                            Description = "Success",
                        });
                    }

                    operation.Responses["200"].Content = GenerateContent(context);
                }
            }
        }
    }

    bool IsTargetReturnType(Type target) =>
        target == typeof(object) ||
        target == typeof(Task<object>) ||
        target == typeof(Task);

    Dictionary<string, OpenApiMediaType> GenerateContent(DocumentFilterContext context) => new()
    {
        ["application/json"] = new OpenApiMediaType
        {
            Schema = context.SchemaGenerator.GenerateSchema(typeof(object), new())
        }
    };
}
