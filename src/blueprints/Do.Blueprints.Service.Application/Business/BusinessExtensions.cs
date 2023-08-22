using Do.Architecture;
using Do.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Do;

public static class BusinessExtensions
{
    public static void AddBusiness(this List<IFeature> source, Func<BusinessConfigurator, IBusinessFeature> configure) => source.Add(configure(new()));

    public static void AddTransientWithFactory<T>(this IServiceCollection source) where T : class
    {
        source.AddSingleton<Func<T>>(sp => () => sp.GetRequiredServiceUsingRequestServices<T>());
        source.AddTransient<T>();
    }
}
