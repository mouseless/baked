using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.CodingStyle.UseBuiltInTypes;

public class ConvertEnumToStringSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum) { return; }

        schema.Type = "string";
        schema.Format = null;
        schema.Enum.Clear();

        foreach (var enumName in Enum.GetNames(context.Type))
        {
            schema.Enum.Add(new OpenApiString(enumName.ToLowerInvariant()));
        }
    }
}