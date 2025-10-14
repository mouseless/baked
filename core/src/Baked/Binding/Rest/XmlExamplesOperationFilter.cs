using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Binding.Rest;

public class XmlExamplesOperationFilter(RequestResponseExamples _examples) : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.ApiDescription.TryGetMappedMethod(out var mappedMethod)) { return; }
        if (!_examples.TryGetValue(mappedMethod.TypeFullName + "." + mappedMethod.MethodName, out var example)) { return; }

        operation.RequestBody?.Content.SetJsonExample(example.Request);
        operation.Responses.TryGetValue("200", out var response);
        response?.Content.SetJsonExample(example.Response);
    }
}