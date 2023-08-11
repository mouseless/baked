using Do.Greeting;
using Do.Greeting.Swagger;

namespace Do;

public static class SwaggerGreetingExtensions
{
    public static SwaggerGreetingFeature Swagger(this GreetingConfigurator _) => new();
}
