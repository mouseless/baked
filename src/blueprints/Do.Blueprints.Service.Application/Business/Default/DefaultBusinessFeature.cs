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
                if (model.Methods.Any(m => m.Name.Equals("With") && m.ReturnType.Equals(model.Type)))
                {
                    services.AddTransientWithFactory(model.Type);
                }
                else
                {
                    services.AddSingleton(model.Type);
                }
            }
        });
    }
}

