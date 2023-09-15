using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Do.Documentation.Default;

public class DefaultDocumentationFeature : IFeature<DocumentationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.CustomSchemaIds(t =>
            {
                string[] splitedNamespace = t.Namespace?.Split(".") ?? new string[0];
                string name = t.IsNested && t.FullName is not null
                    ? t.FullName.Replace($"{t.Namespace}.", string.Empty).Replace("Controller", string.Empty).Replace("+", ".")
                    : t.Name;

                return splitedNamespace.Length > 1
                    ? $"{string.Join('.', splitedNamespace.Skip(1))}.{name}"
                    : name;
            });

            configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
            {
                swaggerGenOptions.MapType<object>(() => new OpenApiSchema { Type = "object" }); // Makes the request content template an object.
                swaggerGenOptions.SchemaFilter<ConvertEnumToStringSchemaFilter>();
                swaggerGenOptions.OperationFilter<NullTypesAreObjectOperationFilter>();
                swaggerGenOptions.DocumentFilter<ObjectResponseDocumentationFilter>();
            });
        });
    }
}
