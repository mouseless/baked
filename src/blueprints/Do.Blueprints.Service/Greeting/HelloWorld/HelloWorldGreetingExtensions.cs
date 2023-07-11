using Do.Blueprints.Service.Greeting;
using Do.Blueprints.Service.Greeting.HelloWorld;

namespace Do;

public static class HelloWorldGreetingExtensions
{
    public static HelloWorldGreeting HelloWorld(this GreetingConfigurator source) => new();
}
