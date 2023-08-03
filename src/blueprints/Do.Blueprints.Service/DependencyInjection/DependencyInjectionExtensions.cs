using Do.Architecture;
using Do.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Do;

public static class DependencyInjectionExtensions
{
    public static void AddDependencyInjection(this List<ILayer> layers) => layers.Add(new DependencyInjectionLayer());
    public static void ConfigureServiceCollection(this LayerConfigurator configurator, Action<IServiceCollection> configuration) => configurator.Configure(configuration);
    public static T GetRequiredServiceUsingRequestServices<T>(this IServiceProvider source) where T : notnull => (T)source.GetRequiredServiceUsingRequestServices(typeof(T));
    public static object GetRequiredServiceUsingRequestServices(this IServiceProvider source, Type serviceType)
    {
        var http = source.GetRequiredService<IHttpContextAccessor>();

        if (http.HttpContext is null) { return source.GetRequiredService(serviceType); }

        return http.HttpContext.RequestServices.GetRequiredService(serviceType);
    }
}
