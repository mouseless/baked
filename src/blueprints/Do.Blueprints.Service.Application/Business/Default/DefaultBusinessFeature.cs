using Do.Architecture;
using Do.Domain.Model;
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
            DomainModel domainModel = configurator.Context.GetDomainModel();

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
                    type.Apply(t =>
                    {
                        services.AddTransientWithFactory(t);
                        type.Interfaces
                            .Where(IsInDomain)
                            .Apply(i => services.AddTransientWithFactory(i, t));
                    });
                }
                else if (type.IsAssignableTo<IScoped>())
                {
                    type.Apply(t =>
                    {
                        services.AddScopedWithFactory(t);
                        type.Interfaces
                            .Where(IsInDomain)
                            .Apply(i => services.AddScopedWithFactory(i, t));
                    });
                }
                else
                {
                    type.Apply(t =>
                    {
                        services.AddSingleton(t);
                        type.Interfaces
                            .Where(IsInDomain)
                            .Apply(i => services.AddSingleton(i, t, forward: true));
                    });
                }
            }

            bool IsInDomain(TypeModel type) => domainModel.Assemblies.Contains(type.Assembly);
        });
    }
}
