using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Orm.AutoMap;

public class IdSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(Id))
        {
            schema.Type = "string";
            schema.Format = null;
            schema.Properties?.Clear();
        }
    }
}