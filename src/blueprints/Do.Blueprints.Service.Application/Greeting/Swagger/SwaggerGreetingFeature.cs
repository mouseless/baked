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
    }
}
