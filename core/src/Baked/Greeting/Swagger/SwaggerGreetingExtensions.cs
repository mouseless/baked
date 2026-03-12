using Baked.Greeting;
using Baked.Greeting.Swagger;

namespace Baked;

public static class SwaggerGreetingExtensions
{
    extension(GreetingConfigurator _)
    {
        public SwaggerGreetingFeature Swagger() =>
            new();
    }
}