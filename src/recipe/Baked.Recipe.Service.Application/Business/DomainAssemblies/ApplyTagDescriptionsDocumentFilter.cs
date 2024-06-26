using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Business.DomainAssemblies;

public class ApplyTagDescriptionsDocumentFilter(TagDescriptions _descriptions)
    : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var documentTags = new HashSet<string>();
        foreach (var (pathKey, path) in swaggerDoc.Paths)
        {
            foreach (var (method, operation) in path.Operations)
            {
                foreach (var tag in operation.Tags)
                {
                    documentTags.Add(tag.Name);
                }
            }
        }

        foreach (var (name, description) in _descriptions.OrderBy(kvp => kvp.Key))
        {
            if (!documentTags.Contains(name)) { continue; }

            swaggerDoc.Tags.Add(new() { Name = name, Description = description });
        }
    }
}