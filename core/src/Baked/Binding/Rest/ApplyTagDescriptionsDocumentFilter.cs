using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Binding.Rest;

public class ApplyTagDescriptionsDocumentFilter(TagDescriptions _descriptions)
    : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var documentTags = new HashSet<string>();
        foreach (var (pathKey, path) in swaggerDoc.Paths)
        {
            if (path.Operations is null) { continue; }

            foreach (var (method, operation) in path.Operations)
            {
                if (operation.Tags is null) { continue; }

                foreach (var tag in operation.Tags)
                {
                    if (tag.Name is null) { continue; }

                    documentTags.Add(tag.Name);
                }
            }
        }

        foreach (var (name, description) in _descriptions.OrderBy(kvp => kvp.Key))
        {
            if (!documentTags.Contains(name)) { continue; }

            swaggerDoc.Tags?.Add(new() { Name = name, Description = description });
        }
    }
}