using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Business.Default;

public class DefaultBusinessFeature : IFeature<BusinessConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            var domainModel = configurator.Context.GetDomainModel();

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

