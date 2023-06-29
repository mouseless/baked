using Do.Architecture;

namespace Do.Test.Blueprints.Service.Greeting.HelloWorld;

public class HelloWorldGreeting : IFeature
{
    public void Configure(ConfigurationTarget target)
    {
        target.ConfigureEndpointRouteBuilder(route =>
        {
            route.MapGet("/", () => "Hello World!");
        });
    }
}
