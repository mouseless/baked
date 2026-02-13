using Humanizer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.CodingStyle.Locatable;

public class ReadOnlyPropertiesSchemaFilter(Dictionary<Type, string> _idPropertyNames)
    : ISchemaFilter
{
    readonly HashSet<Type> _locatableSet = [.. _idPropertyNames.Keys];

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!_locatableSet.Contains(context.Type)) { return; }

        foreach (var (name, property) in schema.Properties)
        {
            var isId = name == _idPropertyNames[context.Type].Camelize();

            property.ReadOnly = !isId;

            // referenced properties (e.g. enums) ignores readonly property,
            // wrapping reference creates a successful workaround and hides
            // these properties in inputs
            if (property.Reference != null && property.ReadOnly)
            {
                var reference = property.Reference;

                property.AllOf = new List<OpenApiSchema> { new OpenApiSchema { Reference = reference } };
                property.Reference = null;
            }
        }
    }
}