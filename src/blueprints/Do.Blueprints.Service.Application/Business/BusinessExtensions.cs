using Do.Architecture;
using Do.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Do;

public static class BusinessExtensions
{
    public static void AddBusiness(this List<IFeature> source, Func<BusinessConfigurator, IFeature<BusinessConfigurator>> configure) => source.Add(configure(new()));

    public static void AddTransientWithFactory<T>(this IServiceCollection source) where T : class => source.AddTransientWithFactory(typeof(T));
    public static void AddTransientWithFactory(this IServiceCollection source, Type type)
    {
        var funcType = typeof(Func<>).MakeGenericType(type);

        source.AddSingleton(funcType, sp => () => sp.GetRequiredServiceUsingRequestServices(type));
        source.AddTransient(type);
    }

    public static void AddScopedWithFactory<T>(this IServiceCollection source) where T : class => source.AddScopedWithFactory(typeof(T));
    public static void AddScopedWithFactory(this IServiceCollection source, Type type)
    {
        var funcType = typeof(Func<>).MakeGenericType(type);

        source.AddSingleton(funcType, sp => sp.GetRequiredServiceUsingRequestServices(type));
        source.AddScoped(type);
    }
}
