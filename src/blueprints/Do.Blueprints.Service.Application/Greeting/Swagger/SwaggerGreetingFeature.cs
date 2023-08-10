using Do.Architecture;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Greeting.Swagger;

public class SwaggerGreetingFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureEndpointRouteBuilder(endpoints =>
        {
            endpoints.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger/index.html");

                return Task.CompletedTask;
            });
        });

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
        });
    }
}
