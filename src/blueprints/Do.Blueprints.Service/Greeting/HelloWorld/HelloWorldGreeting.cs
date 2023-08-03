using Do.Architecture;
using Microsoft.AspNetCore.Builder;

namespace Do.Blueprints.Service.Greeting.HelloWorld;

public class HelloWorldGreetingFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureEndpointRouteBuilder(route =>
        {
            route.MapGet("/", () => "Hello World!");
        });
    }
}
