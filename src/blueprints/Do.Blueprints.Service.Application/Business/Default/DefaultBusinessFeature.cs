using Do.Architecture;
using Do.Domain.Model;
using Do.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Do.Business.Default;

public class DefaultBusinessFeature(List<Assembly> _domainAssemblies)
    : IFeature<BusinessConfigurator>
{
    const BindingFlags _defaultMemberBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainAssemblyCollection(assemblies =>
        {
            foreach (var assembly in _domainAssemblies)
            {
                assemblies.Add(assembly);
            }
        });

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
                    !type.IsBusinessType ||
                    !type.IsPublic ||
                    type.IsInterface ||
                    type.Namespace?.StartsWith("System") == true ||
                    (type.IsSealed && type.IsAbstract) || // if type is static
                    type.IsAbstract ||
                    type.IsValueType ||
                    type.IsGenericMethodParameter ||
                    type.IsGenericTypeParameter ||
                    type.IsAssignableTo<MulticastDelegate>() ||
                    type.IsAssignableTo<Exception>() ||
                    type.IsAssignableTo<Attribute>() ||
                    (type.ContainsGenericParameters && !type.GenericTypeArguments.Any()) ||
                    type.Methods.Contains("<Clone>$") // if type is record
                ) { continue; }

                if (type.Methods.TryGetValue("With", out var with) && with.CanReturn(type))
                {
                    type.Apply(t =>
                    {
                        services.AddTransientWithFactory(t);
                        type.Interfaces
                            .Where(i => i.IsBusinessType)
                            .Apply(i => services.AddTransientWithFactory(i, t));
                    });
                }
                else if (type.IsAssignableTo<IScoped>())
                {
                    type.Apply(t =>
                    {
                        services.AddScopedWithFactory(t);
                        type.Interfaces
                            .Where(i => i.IsBusinessType)
                            .Apply(i => services.AddScopedWithFactory(i, t));
                    });
                }
                else
                {
                    if (type.Properties.Any(p => p.IsPublic)) { continue; }

                    type.Apply(t =>
                    {
                        services.AddSingleton(t);
                        type.Interfaces
                            .Where(i => i.IsBusinessType)
                            .Apply(i => services.AddSingleton(i, t, forward: true));
                    });
                }
            }
        });

        configurator.ConfigureApiModel(api =>
        {
            api.References.AddRange(_domainAssemblies);

            var domainModel = configurator.Context.GetDomainModel();
            foreach (var type in domainModel.Types)
            {
                if (
                    !type.IsBusinessType ||
                    !type.IsPublic ||
                    type.IsInterface ||
                    type.Namespace?.StartsWith("System") == true ||
                    (type.IsSealed && type.IsAbstract) || // if type is static
                    type.IsAbstract ||
                    type.IsValueType ||
                    type.IsGenericMethodParameter ||
                    type.IsGenericTypeParameter ||
                    type.IsAssignableTo<MulticastDelegate>() ||
                    type.IsAssignableTo<Exception>() ||
                    type.IsAssignableTo<Attribute>() ||
                    (type.ContainsGenericParameters && !type.GenericTypeArguments.Any()) ||
                    type.Methods.Contains("<Clone>$") // if type is record
                ) { continue; }

                if (type.Methods.TryGetValue("With", out var with) && with.CanReturn(type))
                {
                }
                else if (type.IsAssignableTo<IScoped>())
                {
                }
                else
                {
                    if (type.Properties.Any(p => p.IsPublic)) { continue; }
                    if (type.FullName is null) { continue; }

                    var singleton = new ControllerModel(type.Name);
                    foreach (var method in type.Methods.Where(mm =>
                                               !mm.IsConstructor &&
                                               mm.Overloads.Length == 1 &&
                                               mm.Overloads[0].IsPublic &&
                                               mm.Overloads[0].Parameters.Count == 0 &&
                                               mm.Overloads[0].ReturnType?.FullName == typeof(void).FullName
                    ))
                    {
                        singleton.Actions.Add(
                            new(
                                Name: method.Name,
                                Method: method.Name.StartsWith("Get") ? HttpMethod.Get : HttpMethod.Post,
                                Route: $"{type.Name.ToLowerInvariant()}/{method.Name.ToLowerInvariant()}",
                                Return: new(),
                                Statements: new(
                                    FindTarget: "target",
                                    InvokeMethod: new(
                                        Name: method.Name
                                    )
                                )
                            )
                            {
                                Parameters = [
                                    new(
                                        From: ParameterModelFrom.Services,
                                        Type: type.FullName,
                                        Name: "target"
                                    )
                                ]
                            }
                        );
                    }

                    api.Controllers.Add(singleton);
                }
            }
        });
    }
}
