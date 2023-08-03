using Do.Blueprints.Service.Greeting;
using Do.Greeting.HelloWorld;

namespace Do;

public static class HelloWorldGreetingExtensions
{
    public static HelloWorldGreetingFeature HelloWorld(this GreetingConfigurator source) => new();
}
