using Do.Architecture;
using Do.HttpServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Do;

public static class HttpServerExtensions
{
    public static void AddHttpServer(this List<ILayer> source) => source.Add(new HttpServerLayer());

    public static void ConfigureMiddlewareCollection(this LayerConfigurator configurator, Action<IMiddlewareCollection> configuration) => configurator.Configure(configuration);
    public static void ConfigureEndpointRouteBuilder(this LayerConfigurator configurator, Action<IEndpointRouteBuilder> configuration) => configurator.Configure(configuration);

    public static void Add<T>(this IMiddlewareCollection source, int order = default) => source.Add(new(app => app.UseMiddleware<T>(), order));
    public static void Add(this IMiddlewareCollection source, Action<IApplicationBuilder> configure, int order = default) => source.Add(new(configure, order));
}
