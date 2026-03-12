using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Nodes;

namespace Baked.CodingStyle.UseBuiltInTypes;

public class ConvertEnumToStringSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum || schema is not OpenApiSchema openApiSchema) { return; }

        openApiSchema.Type = JsonSchemaType.String;
        openApiSchema.Format = null;
        openApiSchema.Enum = [];

        foreach (var enumName in Enum.GetNames(context.Type))
        {
            openApiSchema.Enum.Add(JsonValue.Create(enumName.ToLowerInvariant())!);
        }
    }
}