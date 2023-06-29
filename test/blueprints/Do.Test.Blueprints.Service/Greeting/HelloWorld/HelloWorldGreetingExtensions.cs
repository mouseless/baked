
using Do.Test.Blueprints.Service.Greeting;
using Do.Test.Blueprints.Service.Greeting.HelloWorld;

namespace Do;

public static class HelloWorldGreetingExtensions
{
    public static HelloWorldGreeting HelloWorld(this GreetingConfigurator source) => new();
}
