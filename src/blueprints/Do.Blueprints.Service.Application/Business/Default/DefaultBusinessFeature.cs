using Do.Architecture;
using Do.Orm;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Business.Default;

public class DefaultBusinessFeature : IFeature<BusinessConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAssemblyCollection(assemblies =>
        {
            assemblies.Add(typeof(IQueryContext<>).Assembly);
        });

        configurator.ConfigureServiceCollection(services =>
        {
            var domainModel = configurator.Context.GetDomainModel();
            foreach (var type in domainModel.Types)
            {
                if (!type.IsAbstract && !type.IsValueType)
                {
                    if (type.Methods.Any(m => m.Name.Equals("With") && m.ReturnType?.Equals(type) == true))
                    {
                        type.Apply(t => services.AddTransientWithFactory(t));
                    }
                    else if (type.Constructors.Count == 1)
                    {
                        if (type.Constructors.All(c => c.Parameters.Count > 0 && c.Parameters.All(p => p.ParameterType?.Name.StartsWith("IQueryContext") == true)))
                        {
                            type.Apply(t => services.AddSingleton(t));
                        }
                        else if (type.Constructors.All(c => c.Parameters.Count > 0 && c.Parameters.All(p => p.Name.StartsWith('_'))))
                        {
                            type.Apply(t => services.AddSingleton(t));
                        }
                    }
                }
            }
        });
    }
}
