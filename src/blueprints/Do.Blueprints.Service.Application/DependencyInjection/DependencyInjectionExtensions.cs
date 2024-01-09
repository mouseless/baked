using Do.Architecture;
using Do.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Do;

public static class DependencyInjectionExtensions
{
    public static void AddDependencyInjection(this List<ILayer> source) => source.Add(new DependencyInjectionLayer());
    public static void ConfigureServiceCollection(this LayerConfigurator source, Action<IServiceCollection> configuration) => source.Configure(configuration);

    public static IServiceCollection GetServiceCollection(this ApplicationContext source) => source.Get<IServiceCollection>();

    public static ServiceDescriptor? GetServiceDescriptor<T>(this ApplicationContext source) =>
        source.GetServiceDescriptor(typeof(T));
    public static ServiceDescriptor? GetServiceDescriptor(this ApplicationContext source, Type type) =>
        source.Get<IServiceCollection>().FirstOrDefault(d => d.ServiceType == type);
}
