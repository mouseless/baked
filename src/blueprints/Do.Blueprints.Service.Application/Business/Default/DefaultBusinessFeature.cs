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

            foreach (var type in domainModel.Types)
            {
                if (type.IsAbstract || type.IsValueType) { continue; }

                if (type.Methods.Any(m => m.Name.Equals("With") && m.ReturnType == type))
                {
                    type.Apply(t => services.AddTransientWithFactory(t));
                }
                else if (type.Constructors.Count == 1)
                {
                    if (type.Constructors.FirstOrDefault(c => c.Parameters.Count > 0 && c.Parameters.All(p => p.ParameterType.Name.StartsWith("IQueryContext"))) is not null)
                    {
                        type.Apply(t => services.AddSingleton(t));
                    }
                    else if (type.Constructors.FirstOrDefault(c => c.Parameters.Count > 0 && c.Parameters.All(p => p.Name.StartsWith('_'))) is not null)
                    {
                        type.Apply(t => services.AddSingleton(t));
                    }
                }
            }
        });
    }
}
