using Do.Architecture;

namespace Do.Test.Blueprints.Service.Greeting.HelloWorld;

public class HelloWorldGreeting : IFeature
{
    public void Configure(object target)
    {
        if (target is WebApplication app)
        {
            app.MapGet("/", () => "Hello World!");
        }
    }
}
