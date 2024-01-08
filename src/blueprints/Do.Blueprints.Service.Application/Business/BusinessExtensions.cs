using Do.Architecture;
using Do.Business;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Do;

public static class BusinessExtensions
{
    public static void AddBusiness(this List<IFeature> source, Func<BusinessConfigurator, IFeature<BusinessConfigurator>> configure) => source.Add(configure(new()));

    static readonly MethodInfo _addTransientWithFactory = typeof(BusinessExtensions).GetMethod(nameof(AddTransientWithFactory), 1, [typeof(IServiceCollection)]) ??
            throw new Exception("AddTransientWithFactory<T> should have existed");

    public static void AddTransientWithFactory(this IServiceCollection source, Type type) =>
        _addTransientWithFactory.MakeGenericMethod(type).Invoke(null, new object[] { source });

    public static void AddTransientWithFactory<TService>(this IServiceCollection source) where TService : class =>
        source.AddTransientWithFactory<TService, TService>();

    public static void AddTransientWithFactory<TService, TImplementation>(this IServiceCollection source)
        where TService : class
        where TImplementation : class, TService
    {
        source.AddSingleton<Func<TService>>(sp => () => sp.GetRequiredServiceUsingRequestServices<TService>());
        source.AddTransient<TService, TImplementation>();
    }

    static readonly MethodInfo _addScopedWithFactory = typeof(BusinessExtensions).GetMethod(nameof(AddScopedWithFactory), 1, new Type[] { typeof(IServiceCollection) }) ??
            throw new Exception("AddScopedWithFactory<T> should have existed");

    public static void AddScopedWithFactory(this IServiceCollection source, Type type) =>
        _addScopedWithFactory.MakeGenericMethod(type).Invoke(null, new object[] { source });
    public static void AddScopedWithFactory<TService>(this IServiceCollection source) where TService : class =>
        source.AddScopedWithFactory<TService, TService>();

    public static void AddScopedWithFactory<TService, TImplementation>(this IServiceCollection source)
        where TService : class
        where TImplementation : class, TService
    {
        source.AddSingleton<Func<TService>>(sp => () => sp.GetRequiredServiceUsingRequestServices<TService>());
        source.AddScoped<TService, TImplementation>();
    }
}
