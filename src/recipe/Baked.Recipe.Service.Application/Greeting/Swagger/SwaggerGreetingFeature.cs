using Baked.Architecture;
using Microsoft.AspNetCore.Builder;

namespace Baked.Greeting.Swagger;

public class SwaggerGreetingFeature : IFeature<GreetingConfigurator>
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