using Baked.Architecture;
using Baked.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class DependencyInjectionExtensions
{
    public static void AddDependencyInjection(this List<ILayer> source) =>
        source.Add(new DependencyInjectionLayer());

    public static IServiceCollection GetServiceCollection(this ApplicationContext source) =>
        source.Get<IServiceCollection>();

    public static IServiceProvider GetServiceProvider(this ApplicationContext source) =>
        source.Get<IServiceProvider>();

    public static void ConfigureServiceCollection(this LayerConfigurator source, Action<IServiceCollection> configuration) =>
        source.Configure(configuration);

    public static void ConfigureServiceProvider(this LayerConfigurator source, Action<IServiceProvider> configuration) =>
        source.Configure(configuration);
}