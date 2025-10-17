using Baked.Binding.Rest;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.CodingStyle.CommandPattern;

public class XmlExamplesFromClassOperationFilter(IEnumerable<string> actionNames, Dictionary<string, RequestResponseExampleData> _methodExamplesDictionary)
    : IOperationFilter
{
    readonly HashSet<string> _actionNames = [.. actionNames];

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.ApiDescription.TryGetMappedMethod(out var mappedMethod)) { return; }
        if (!_actionNames.Contains(mappedMethod.MethodName)) { return; }
        if (!_methodExamplesDictionary.TryGetValue(mappedMethod.TypeFullName, out var example)) { return; }

        operation.RequestBody?.Content.SetJsonExample(example.Request);
        operation.Responses.TryGetValue("200", out var response);
        response?.Content.SetJsonExample(example.Response);
    }
}