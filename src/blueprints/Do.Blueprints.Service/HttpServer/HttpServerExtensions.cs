using Do.Architecture;
using Do.HttpServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Do;

public static class HttpServerExtensions
{
    public static void AddHttpServer(this List<ILayer> source) => source.Add(new HttpServerLayer());
    public static void ConfigureApplicationBuilder(this LayerConfigurator configurator, Action<IApplicationBuilder> configuration) => configurator.Configure(configuration);
    public static void ConfigureEndpointRouteBuilder(this LayerConfigurator configurator, Action<IEndpointRouteBuilder> configuration) => configurator.Configure(configuration);
}
