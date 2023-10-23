using Do.Architecture;
using Do.Business;

namespace Do.Test.Business.Default;

public class DefaultBusinessFeature : IFeature<BusinessConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddTransientWithFactory<Entity>();
            services.AddSingleton<Entities>();
            services.AddSingleton<Singleton>();
        });
    }
}

public static class BusinessFeatureExtensions
{
    public static void AddTransientWithFactoryFromType(this IServiceCollection services, Type type)
    {
        var addsingletonMethod = typeof(BusinessExtensions)
            .GetMethods()
            .FirstOrDefault(m =>
                m.Name == nameof(BusinessExtensions.AddTransientWithFactory) &&
                m.GetGenericArguments().Length == 1
            ) ?? throw new Exception("AddTransientWithFactory should not be null");

        var generic = addsingletonMethod.MakeGenericMethod(type);

        generic.Invoke(null, new object[] { services });
    }
}
