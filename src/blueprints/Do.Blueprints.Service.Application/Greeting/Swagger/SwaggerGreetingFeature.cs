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
                if(!t.IsNested)
                {
                    return t.Name;
                }

                return t.FullName?
                        .Replace($"{t.Namespace}.", "")
                        .Replace("Controller", "")
                        .Replace(".", "_")
                        .Replace("+", "_");
            });
        });
    }
}
