using Baked.Architecture;
using Microsoft.AspNetCore.Builder;

namespace Baked;

public static class SwaggerGreetingFeature
{
    public static IFeature<GreetingFeature.Configurator> Swagger(this GreetingFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.HttpServer.ConfigureEndpointRouteBuilder(endpoints =>
        {
            endpoints.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger/index.html");

                return Task.CompletedTask;
            });
        });
    });
}