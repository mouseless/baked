using Humanizer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Binding.Rest;

public class RemoveNonPublicPropertiesSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        foreach (var property in context.Type.GetProperties().Where(p => !p.IsOriginallyPublic()))
        {
            schema.Properties.Remove(property.Name.Camelize());
        }
    }
}