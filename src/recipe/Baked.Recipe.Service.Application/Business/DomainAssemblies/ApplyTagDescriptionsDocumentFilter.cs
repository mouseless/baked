using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Business.DomainAssemblies;

public class ApplyTagDescriptionsDocumentFilter(TagDescriptions _descriptions)
    : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var (name, description) in _descriptions.OrderBy(kvp => kvp.Key))
        {
            swaggerDoc.Tags.Add(new() { Name = name, Description = description });
        }
    }
}