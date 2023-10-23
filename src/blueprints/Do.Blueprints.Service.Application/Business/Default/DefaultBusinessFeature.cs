using Do.Architecture;
using Do.Domain.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Business.Default;

public class DefaultBusinessFeature : IFeature<BusinessConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            var domainModel = configurator.Context.Get<DomainModel>();

            foreach (var (type, model) in domainModel.TypeModels)
            {
                if (model.HasMethod(m => m.Name.Equals("With") && m.ReturnType.Equals(type)))
                {
                    services.AddTransientWithFactoryFromType(type);
                }
                else
                {
                    services.AddSingleton(type);
                }
            }
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
