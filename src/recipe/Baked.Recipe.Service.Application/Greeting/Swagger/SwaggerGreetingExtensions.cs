using Baked.Greeting;
using Baked.Greeting.Swagger;

namespace Baked;

public static class SwaggerGreetingExtensions
{
    public static SwaggerGreetingFeature Swagger(this GreetingConfigurator _) =>
        new();
}