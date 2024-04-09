using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Do.CodingStyle.ObjectAsJson;

public class NullTypesAreObjectSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        foreach (var property in schema.Properties.Values)
        {
            if (property.Type is null)
            {
                property.Type = "object";
            }
        }
    }
}