using Do.Architecture;

namespace Do.Test.Blueprints.Service.Greeting.HelloWorld;

public class HelloWorldGreeting : IFeature
{
    public void Configure(object target)
    {
        target.ConfigureEndpointRouteBuilder(route =>
        {
            route.MapGet("/", () => "Hello World!");
        });
    }
}
