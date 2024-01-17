using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Do.Business.Default;

public class DefaultBusinessFeature : IFeature<BusinessConfigurator>
{
    const BindingFlags _defaultMemberBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainBuilderOptions(options =>
        {
            options.ConstuctorBindingFlags = _defaultMemberBindingFlags;
            options.MethodBindingFlags = _defaultMemberBindingFlags;
            options.PropertyBindingFlags = _defaultMemberBindingFlags;

            options.TypeIsBuiltConventions.Add(type => type.Namespace?.StartsWith("System") == false);
            options.TypeIsBuiltConventions.Add(type => !type.IsAssignableTo(typeof(Attribute)));
        });

        configurator.ConfigureServiceCollection(services =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            foreach (var type in domainModel.Types)
            {
                if (
                    type.Namespace?.StartsWith("System") == true ||
                    type.IsSealed && type.IsAbstract ||
                    type.IsAbstract ||
                    type.IsValueType ||
                    type.IsGenericMethodParameter ||
                    type.IsAssignableTo<Exception>()
                ) { continue; }

                if (type.Methods.TryGetValue("With", out var method) && method.Overloads.All(o => o.ReturnType?.Id == type.Id))
                {
                    type.Apply(t => services.AddTransientWithFactory(t));
                }
                else
                {
                    if (type.Constructor.Overloads.All(c => c.Parameters.All(p => !p.ParameterType.IsValueType)))
                    {
                        type.Apply(t => services.AddSingleton(t));
                    }
                }
            }
        });
    }
}
