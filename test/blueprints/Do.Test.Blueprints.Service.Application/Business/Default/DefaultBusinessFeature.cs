using Do.Architecture;
using Do.Business;
using static Do.Domain.DomainModel;

namespace Do.Test.Business.Default;

public class DefaultBusinessFeature : IFeature<BusinessConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        //    configurator.ConfigureServiceCollection(services =>
        //    {
        //        services.AddTransientWithFactory<Entity>();
        //        services.AddSingleton<Entities>();
        //        services.AddSingleton<Singleton>();
        //    });

        configurator.ConfigureDomainDescriptor(descriptor =>
        {
            descriptor.AddAssemblyOfType<Entity>();

            descriptor.AddType<Entity>();
            descriptor.AddType<Entities>();
            descriptor.AddType<Singleton>();
        });

        configurator.ConfigureDomainServiceDescriptor(serviceDescriptor =>
        {
            var services = configurator.Context.GetServiceCollection();

            serviceDescriptor
                .AddConvention<TypeModel>(
                    appliesTo: (model) => model is TypeModel typeModel && typeModel.Type.Name == "Entity",
                    apply: (model) => services.AddTransientWithFactoryFromType(model.Type)
                )
                .AddConvention<TypeModel>(
                    appliesTo: (model) => model is TypeModel typeModel && typeModel.Type.Name != "Entity",
                    apply: (model) => services.AddSingleton(model.Type)
                )
            ;
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
