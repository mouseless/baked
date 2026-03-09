using Humanizer;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Binding.Rest;

public class RemoveNonPublicPropertiesSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties is null) { return; }

        foreach (var property in context.Type.GetProperties().Where(p => !p.IsOriginallyPublic()))
        {
            schema.Properties.Remove(property.Name.Camelize());
        }
    }
}