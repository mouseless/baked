using Do.Architecture;
using Do.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Do;

public static class WebExtensions
{
    public static void AddWeb(this List<ILayer> source) => source.Add(new WebLayer());
    public static void ConfigureApplicationBuilder(this LayerConfigurator configurator, Action<IApplicationBuilder> configuration) => configurator.Configure(configuration);
    public static void ConfigureEndpointRouteBuilder(this LayerConfigurator configurator, Action<IEndpointRouteBuilder> configuration) => configurator.Configure(configuration);
}
