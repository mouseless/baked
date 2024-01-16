using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Do.Business.Default;

public class DefaultBusinessFeature : IFeature<BusinessConfigurator>
{
    const BindingFlags _defaultMemberBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainOptions(options =>
        {
            options.ConstuctorBindingFlags = _defaultMemberBindingFlags;
            options.MethodBindingFlags = _defaultMemberBindingFlags;
            options.PropertyBindingFlags = _defaultMemberBindingFlags;

            options.TypeIsBuiltConventions.Add(type => type.Namespace?.StartsWith("System") == false);
        });

        configurator.ConfigureServiceCollection(services =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            foreach (var type in domainModel.Types)
            {
                if (
                    type.IsSystemType ||
                    type.IsStatic ||
                    type.IsAbstract ||
                    type.IsValueType ||
                    type.IsGenericMethodParameter ||
                    type.IsAssignableTo<Exception>()
                ) { continue; }

                if (type.Methods.TryGetValue("With", out var method) && method.ReturnType == type)
                {
                    type.Apply(t => services.AddTransientWithFactory(t));
                }
                else
                {
                    if (type.Constructors.All(c => c.Parameters.All(p => !p.ParameterType.IsValueType)))
                    {
                        type.Apply(t => services.AddSingleton(t));
                    }
                }
            }
        });
    }
}
