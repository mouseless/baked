using Baked.Domain.Model;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.CodingStyle.CommandPattern;

public class XmlExamplesFromClassOperationFilter(IEnumerable<string> actionNames, DomainModel _domain)
    : IOperationFilter
{
    readonly HashSet<string> _actionNames = actionNames.ToHashSet();

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.ApiDescription.TryGetMappedMethod(out var mappedMethod)) { return; }
        if (!_domain.Types.TryGetValue(mappedMethod.TypeFullName, out var type)) { return; }
        if (!type.TryGetMembers(out var members)) { return; }
        if (!_actionNames.Contains(mappedMethod.MethodName)) { return; }

        operation.RequestBody?.Content.SetJsonExample(members.Documentation, @for: "request");
        operation.Responses.TryGetValue("200", out var response);
        response?.Content.SetJsonExample(members.Documentation, @for: "response");
    }
}