using Do.Architecture;
using Do.Domain.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Do.Business.Default;

public class DefaultBusinessFeature : IFeature<BusinessConfigurator>
{
    const BindingFlags _defaultMemberBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

    delegate void RegisterInterface(Type @interface, Type service);
    delegate void RegisterService(Type service);

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
                    AddService(type, services.AddTransientWithFactory, services.AddTransientWithFactory);
                }
                else if (type.IsAssignableTo<IScoped>())
                {
                    AddService(type, services.AddScopedWithFactory, services.AddScopedWithFactory);
                }
                else
                {
                    AddService(type, type => services.AddSingleton(type), services.ForwardService);
                }
            }

            void AddService(TypeModel type, RegisterService registerService, RegisterInterface registerInterface)
            {
                type.Apply(t => registerService(t));

                foreach (var @interface in type.Interfaces)
                {
                    if (domainModel.Assemblies.Contains(@interface.Assembly))
                    {
                        type.Apply(tservice => @interface.Apply(t => registerInterface(t, tservice)));
                    }
                }
            }
        });
    }
}
