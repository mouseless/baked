using Do.Architecture;

namespace Do.Test.Blueprints.Service.Greeting.HelloWorld;

public class HelloWorldGreeting : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureEndpointRouteBuilder(route =>
        {
            route.MapGet("/", () => "Hello World!");
        });
    }
}
