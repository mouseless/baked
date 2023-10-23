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

            foreach (var assemblyModel in domainModel.AssemblyModels)
            {
                foreach (var model in assemblyModel.TypeModels)
                {
                    if (model.HasConstructor(c => !c.IsPublic && c.Parameters is null) && model.HasMethod(m => m.Name.Equals("With") && m.ReturnType.Equals(model.Type)))
                    {
                        services.AddTransientWithFactoryForType(model.Type);
                    }
                    else
                    {
                        services.AddSingleton(model.Type);
                    }
                }
            }
        });
    }
}

public static class BusinessFeatureExtensions
{
    public static void AddTransientWithFactoryForType(this IServiceCollection services, Type type)
    {
        var addTransientMethod = typeof(BusinessExtensions)
            .GetMethods()
            .FirstOrDefault(m =>
                m.Name == nameof(BusinessExtensions.AddTransientWithFactory) &&
                m.GetGenericArguments().Length == 1
            ) ?? throw new Exception("AddTransientWithFactory should not be null");

        var generic = addTransientMethod.MakeGenericMethod(type);

        generic.Invoke(null, new object[] { services });
    }
}
