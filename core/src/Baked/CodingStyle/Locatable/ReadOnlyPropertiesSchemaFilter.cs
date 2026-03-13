using Humanizer;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.CodingStyle.Locatable;

public class ReadOnlyPropertiesSchemaFilter(Dictionary<Type, string> _idPropertyNames)
    : ISchemaFilter
{
    readonly HashSet<Type> _locatableSet = [.. _idPropertyNames.Keys];

    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (!_locatableSet.Contains(context.Type)) { return; }
        if (schema.Properties is null) { return; }

        foreach (var (name, property) in schema.Properties)
        {
            var isId = name == _idPropertyNames[context.Type].Camelize();

            // referenced properties (e.g. enums) ignores readonly property,
            // wrapping reference creates a successful workaround and hides
            // these properties in inputs
            if (property is OpenApiSchema concreteProperty)
            {
                concreteProperty.ReadOnly = !isId;
            }
            else if (property is OpenApiSchemaReference referenceProperty)
            {
                schema.Properties[name] = new OpenApiSchema
                {
                    ReadOnly = !isId,
                    AllOf = [referenceProperty]
                };
            }
        }
    }
}