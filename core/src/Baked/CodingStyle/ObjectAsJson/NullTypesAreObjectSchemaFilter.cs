using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.CodingStyle.ObjectAsJson;

public class NullTypesAreObjectSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext _)
    {
        if (schema.Properties is null) { return; }

        foreach (var property in schema.Properties.Values.OfType<OpenApiSchema>())
        {
            if (property.Type is null)
            {
                property.Type = JsonSchemaType.Object;
            }
        }
    }
}