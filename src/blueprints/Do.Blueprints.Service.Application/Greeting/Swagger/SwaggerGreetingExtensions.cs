using Do.Greeting;
using Do.Greeting.Swagger;

namespace Do;

public static class SwaggerGreetingExtensions
{
    public static IGreetingFeature Swagger(this GreetingConfigurator _) => new SwaggerGreetingFeature();
}
