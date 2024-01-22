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
        });

        configurator.ConfigureServiceCollection(services =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            foreach (var type in domainModel.Types)
            {
                if (
                    type.Namespace?.StartsWith("System") == true ||
                    (type.IsSealed && type.IsAbstract) || // if type is static
                    type.IsAbstract ||
                    type.IsValueType ||
                    type.IsGenericMethodParameter ||
                    type.IsGenericTypeParameter ||
                    type.IsAssignableTo<Exception>() ||
                    type.IsAssignableTo<Attribute>() ||
                    type.Methods.TryGetValue("<Clone>$", out _) // if type is record
                ) { continue; }

                if (type.Methods.TryGetValue("With", out var method) && method.Overloads.All(o => o.ReturnType == type))
                {
                    type.Apply(t => services.AddTransientWithFactory(t));

                    foreach (var @interface in type.Interfaces)
                    {
                        if (domainModel.Assemblies.Contains(@interface.Assembly))
                        {
                            type.Apply(t => @interface.Apply(tservice => services.AddTransientWithFactory(tservice, t)));
                        }
                    }
                }
                else
                {
                    type.Apply(t => services.AddSingleton(t));

                    foreach (var @interface in type.Interfaces)
                    {
                        if (domainModel.Assemblies.Contains(@interface.Assembly))
                        {
                            type.Apply(t => @interface.Apply(tservice => services.AddSingleton(tservice, sp => sp.GetRequiredServiceUsingRequestServices(t))));
                        }
                    }
                }
            }
        });
    }
}
