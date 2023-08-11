using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Documentation.Default;

public class DefaultDocumentationFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.CustomSchemaIds(t =>
            {
                string[] splitedNamespace = t.Namespace!.Split(".");
                string name = t.Name;

                if (t.IsNested)
                {
                    name = t.FullName?
                        .Replace($"{t.Namespace}.", "")
                        .Replace("Controller", "")
                        .Replace("+", ".")!;
                }

                return splitedNamespace.Length > 1
                    ? $"{string.Join('.', splitedNamespace.Skip(1))}.{name}"
                    : name;
            });

            configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
            {
                swaggerGenOptions.OperationFilter<NullTypesAreObjectOperationFilter>();
            });
        });
    }
}
