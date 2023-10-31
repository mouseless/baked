using Do.Architecture;
using Do.Business;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Do;

public static class BusinessExtensions
{
    public static void AddBusiness(this List<IFeature> source, Func<BusinessConfigurator, IFeature<BusinessConfigurator>> configure) => source.Add(configure(new()));

    static readonly MethodInfo _addTransientWithFactory = typeof(BusinessExtensions).GetMethod(nameof(AddTransientWithFactory), 1, new Type[] { typeof(IServiceCollection) }) ??
            throw new Exception("AddTransientWithFactory<T> should have existed");

    public static void AddTransientWithFactory(this IServiceCollection source, Type type) =>
        _addTransientWithFactory.MakeGenericMethod(type).Invoke(null, new object[] { source });

    public static void AddTransientWithFactory<T>(this IServiceCollection source) where T : class
    {
        source.AddSingleton<Func<T>>(sp => () => sp.GetRequiredServiceUsingRequestServices<T>());
        source.AddTransient<T>();
    }

    static readonly MethodInfo _addScopedWithFactory = typeof(BusinessExtensions).GetMethod(nameof(AddScopedWithFactory), 1, new Type[] { typeof(IServiceCollection) }) ??
            throw new Exception("AddScopedWithFactory<T> should have existed");

    public static void AddScopedWithFactory(this IServiceCollection source, Type type) =>
        _addScopedWithFactory.MakeGenericMethod(type).Invoke(null, new object[] { source });
    public static void AddScopedWithFactory<T>(this IServiceCollection source) where T : class
    {
        source.AddSingleton<Func<T>>(sp => () => sp.GetRequiredServiceUsingRequestServices<T>());
        source.AddScoped<T>();
    }
}
