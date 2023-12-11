using Do.Architecture;
using Do.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Do;

public static class BusinessExtensions
{
    public static void AddBusiness(this List<IFeature> source, Func<BusinessConfigurator, IFeature<BusinessConfigurator>> configure) => source.Add(configure(new()));

    public static void AddTransientWithFactory<TService>(this IServiceCollection source) where TService : class =>
        source.AddTransientWithFactory<TService, TService>();

    public static void AddTransientWithFactory<TService, TImplementation>(this IServiceCollection source)
        where TService : class
        where TImplementation : class, TService
    {
        source.AddSingleton<Func<TService>>(sp => () => sp.GetRequiredServiceUsingRequestServices<TImplementation>());
        source.AddTransient<TImplementation>();
    }

    public static void AddScopedWithFactory<TService>(this IServiceCollection source) where TService : class =>
        source.AddScopedWithFactory<TService, TService>();

    public static void AddScopedWithFactory<TService, TImplementation>(this IServiceCollection source)
        where TService : class
        where TImplementation : class, TService
    {
        source.AddSingleton<Func<TService>>(sp => () => sp.GetRequiredServiceUsingRequestServices<TImplementation>());
        source.AddScoped<TImplementation>();
    }
}
