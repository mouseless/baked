using Baked.Architecture;
using Baked.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class DependencyInjectionExtensions
{
    public static void AddDependencyInjection(this List<ILayer> layers) =>
        layers.Add(new DependencyInjectionLayer());

    public static IServiceCollection GetServiceCollection(this ApplicationContext context) =>
        context.Get<IServiceCollection>();

    public static IServiceProvider GetServiceProvider(this ApplicationContext context) =>
        context.Get<IServiceProvider>();

    public static void ConfigureServiceCollection(this LayerConfigurator configurator, Action<IServiceCollection> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureServiceProvider(this LayerConfigurator configurator, Action<IServiceProvider> configuration) =>
        configurator.Configure(configuration);
}