using Do.Architecture;
using Do.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Test.Benchmark;

public class BenchmarkBusinessFeature : IFeature<BusinessConfigurator>
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
                    if (!model.Type.IsAssignableTo(typeof(IFeature)) && !model.Type.Name.EndsWith("Extensions"))
                    {
                        services.AddSingleton(model.Type);
                    }
                }
            }
        });
    }
}

