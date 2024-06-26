using Baked.Domain.Model;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Business.DomainAssemblies;

public class XmlExamplesOperationFilter(DomainModel _domain) : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.ApiDescription.TryGetMappedMethod(out var mappedMethod)) { return; }
        if (!_domain.Types.TryGetValue(mappedMethod.TypeFullName, out var type)) { return; }
        if (!type.TryGetMembers(out var members)) { return; }

        var method = members.Methods[mappedMethod.MethodName];

        operation.RequestBody?.Content.SetJsonExample(method.Documentation, @for: "request");
        operation.Responses.TryGetValue("200", out var response);
        response?.Content.SetJsonExample(method.Documentation, @for: "response");
    }
}