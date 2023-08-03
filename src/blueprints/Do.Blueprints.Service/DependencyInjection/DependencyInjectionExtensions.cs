using Do.Architecture;
using Do.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Do;

public static class DependencyInjectionExtensions
{
    public static void AddDependencyInjection(this List<ILayer> layers) => layers.Add(new DependencyInjectionLayer());
    public static void ConfigureServiceCollection(this LayerConfigurator configurator, Action<IServiceCollection> configuration) => configurator.Configure(configuration);
}
