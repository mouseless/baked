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
        var apiDescriptions = context.ApiDescriptions;
        foreach (var apiDescription in apiDescriptions)
        {
            var controllerActionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                if (controllerActionDescriptor.MethodInfo.ReturnType == typeof(object) || controllerActionDescriptor.MethodInfo.ReturnType == typeof(Task<object>))
                {
                    var path = "/" + apiDescription!.RelativePath!.TrimEnd('/');
                    var operation = swaggerDoc.Paths[path].Operations[_operationTypes[apiDescription.HttpMethod!]];

                    if (!operation.Responses.Any())
                    {
                        operation.Responses.Add("200", new OpenApiResponse
                        {
                            Description = "Success",
                            Content = new Dictionary<string, OpenApiMediaType>
                            {
                                ["application/json"] = new OpenApiMediaType
                                {
                                    Schema = context.SchemaGenerator.GenerateSchema(typeof(object), new())
                                }
                            }
                        });
                    }
                    else
                    {
                        operation.Responses["200"].Content = new Dictionary<string, OpenApiMediaType>
                        {
                            ["application/json"] = new OpenApiMediaType
                            {
                                Schema = context.SchemaGenerator.GenerateSchema(typeof(object), new())
                            }
                        };
                    }
                }
            }
        }
    }
}
