using Baked.Domain.Model;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Business.DomainAssemblies;

public class XmlExamplesOperationFilter(DomainModel _domain) : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var mappedMethod = context.ApiDescription.CustomAttributes().OfType<MappedMethodAttribute>().SingleOrDefault();
        if (mappedMethod is null) { return; }
        if (!_domain.Types.TryGetValue(mappedMethod.TypeFullName, out var type)) { return; }
        if (!type.TryGetMembers(out var members)) { return; }

        var method = members.Methods[mappedMethod.MethodName];
        if (method.Documentation is null) { return; }

        var requestExample = method.Documentation.SelectSingleNode("example[@for='rest-api']/code[@for='request']");
        if (requestExample is not null)
        {
            operation.RequestBody.Content["application/json"].Example = OpenApiAnyFactory.CreateFromJson(requestExample.InnerText);
        }

        var responseExample = method.Documentation.SelectSingleNode("example[@for='rest-api']/code[@for='response']");
        if (responseExample is not null)
        {
            operation.Responses["200"].Content["application/json"].Example = OpenApiAnyFactory.CreateFromJson(responseExample.InnerText);
        }
    }
}