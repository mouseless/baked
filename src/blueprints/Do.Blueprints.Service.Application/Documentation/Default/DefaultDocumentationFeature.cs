using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Documentation.Default;

public class DefaultDocumentationFeature : IFeature<DocumentationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.CustomSchemaIds(t =>
            {
                string[] splitedNamespace = t.Namespace?.Split(".") ?? [];
                string name = t.IsNested && t.FullName is not null
                    ? t.FullName.Replace($"{t.Namespace}.", string.Empty).Replace("Controller", string.Empty).Replace("+", ".")
                    : t.Name;

                return splitedNamespace.Length > 1
                    ? $"{string.Join('.', splitedNamespace.Skip(1))}.{name}"
                    : name;
            });
        });
    }
}