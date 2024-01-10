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
            foreach (var model in domainModel.Types)
            {
                if (!model.IsAbstract && !model.IsValueType)
                {
                    if (model.Methods.Any(m => m.Name.Equals("With") && m.ReturnType.Equals(model.Type)))
                    {
                        services.AddTransientWithFactory(model.Type);
                    }
                    else if (model.Properties.All(p => !p.IsPublic))
                    {
                        services.AddSingleton(model.Type);
                    }
                }
            }
        });
    }
}
