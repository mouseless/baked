using Do.Architecture;
using Do.Test.Blueprints.Service.Web;

namespace Do;

public static class WebExtensions
{
    public static void AddWeb(this List<ILayer> source) => source.Add(new WebLayer());
    public static void ConfigureApplicationBuilder(this ConfigurationTarget target, Action<IApplicationBuilder> configuration) => target.Configure(configuration);
    public static void ConfigureEndpointRouteBuilder(this ConfigurationTarget target, Action<IEndpointRouteBuilder> configuration) => target.Configure(configuration);
}
