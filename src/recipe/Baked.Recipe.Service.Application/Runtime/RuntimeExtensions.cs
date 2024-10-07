using Baked.Architecture;
using Baked.Runtime;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Baked;

public static class RuntimeExtensions
{
    public static void AddRuntime(this List<ILayer> layers) =>
        layers.Add(new RuntimeLayer());

    public static IServiceCollection GetServiceCollection(this ApplicationContext context) =>
        context.Get<IServiceCollection>();

    public static IServiceProvider GetServiceProvider(this ApplicationContext context) =>
        context.Get<IServiceProvider>();

    public static void ConfigureServiceCollection(this LayerConfigurator configurator, Action<IServiceCollection> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureServiceProvider(this LayerConfigurator configurator, Action<IServiceProvider> configuration) =>
        configurator.Configure(configuration);

    public static void AddFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var serviceAdderType = assembly.GetExportedTypes().SingleOrDefault(t => t.IsAssignableTo(typeof(IServiceAdder))) ?? throw new("`IServiceAdder` implementation not found");
        var serviceAdder = (IServiceAdder?)Activator.CreateInstance(serviceAdderType) ?? throw new($"Cannot create instance of {serviceAdderType}");

        serviceAdder.AddServices(services);
    }

    public static IServiceProvider TheServiceProvider(this Stubber giveMe) =>
        giveMe.Spec.Context.GetServiceProvider();

    public static T The<T>(this Stubber giveMe) where T : notnull =>
        giveMe.TheServiceProvider().GetRequiredService<T>();

    public static object The(this Stubber giveMe, Type type) =>
        giveMe.TheServiceProvider().GetRequiredService(type);

    public static T An<T>(this Stubber giveMe) where T : notnull =>
        giveMe.TheServiceProvider().GetRequiredService<T>();

    public static object An(this Stubber giveMe, Type type) =>
        giveMe.TheServiceProvider().GetRequiredService(type);

    public static T A<T>(this Stubber giveMe) where T : notnull =>
        giveMe.TheServiceProvider().GetRequiredService<T>();

    public static object A(this Stubber giveMe, Type type) =>
        giveMe.TheServiceProvider().GetRequiredService(type);
}