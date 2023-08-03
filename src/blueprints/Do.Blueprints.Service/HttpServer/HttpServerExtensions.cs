using Do.Architecture;
using Do.HttpServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Do;

public static class HttpServerExtensions
{
    public static void AddHttpServer(this List<ILayer> source) => source.Add(new HttpServerLayer());

    public static WebApplicationBuilder GetWebApplicationBuilder(this ApplicationContext source) => source.Get<WebApplicationBuilder>();
    public static WebApplication GetWebApplication(this ApplicationContext source) => source.Get<WebApplication>();

    public static void ConfigureMiddlewareCollection(this LayerConfigurator configurator, Action<IMiddlewareCollection> configuration) => configurator.Configure(configuration);
    public static void ConfigureEndpointRouteBuilder(this LayerConfigurator configurator, Action<IEndpointRouteBuilder> configuration) => configurator.Configure(configuration);

    public static void Add<T>(this IMiddlewareCollection source, int order = default) => source.Add(new(app => app.UseMiddleware<T>(), order));
    public static void Add(this IMiddlewareCollection source, Action<IApplicationBuilder> configure, int order = default) => source.Add(new(configure, order));

    public static T GetRequiredServiceUsingRequestServices<T>(this IServiceProvider source) where T : notnull => (T)source.GetRequiredServiceUsingRequestServices(typeof(T));
    public static object GetRequiredServiceUsingRequestServices(this IServiceProvider source, Type serviceType)
    {
        var http = source.GetRequiredService<IHttpContextAccessor>();

        if (http.HttpContext is null) { return source.GetRequiredService(serviceType); }

        return http.HttpContext.RequestServices.GetRequiredService(serviceType);
    }
}
