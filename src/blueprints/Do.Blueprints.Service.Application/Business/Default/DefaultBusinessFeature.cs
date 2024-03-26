using Do.Architecture;
using Do.Domain.Model;
using Do.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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

            options.AddBaseType.Add(t => t.IsDomainType);
            options.AddBaseType.Add(t => t.ReflectedType.BaseType == typeof(Task));
            options.AddProperties.Add(t => t.IsDomainType);
            options.AddMethods.Add(t => t.IsDomainType);
            options.AddInterfaces.Add(t => t.IsDomainType);
            options.AddConstructor.Add(t => t.IsDomainType);
        });

        configurator.ConfigureDomainIndexers(indexers =>
        {
            indexers.AddAttributeIndexer<TransientAttribute>();
            indexers.AddAttributeIndexer<ScopedAttribute>();
            indexers.AddAttributeIndexer<SingletonAttribute>();
            indexers.AddAttributeIndexer<ApiMethodAttribute>();
            indexers.AddAttributeIndexer<DomainServiceAttribute>();
        });

        configurator.ConfigureDomainMetaData(metadata =>
        {
            metadata
                .Type
                    .Add(
                        add: new DataClassAttribute(),
                        when: type => type.Methods.Contains("<Clone>$"), // if type is record
                        order: int.MinValue
                    )
                    .Add(
                        add: new DomainServiceAttribute(),
                        when: type =>
                            !(
                                !type.IsDomainType ||
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
                                type.Has<DataClassAttribute>()
                            ),
                        order: 1
                    )
                    .Add(
                        add: new TransientAttribute(),
                        when: type => type.Has<DomainServiceAttribute>() && type.Methods.TryGetValue("With", out var with) && with.CanReturn(type),
                        order: 2
                    )
                    .Add(
                        add: new ScopedAttribute(),
                        when: type => type.Has<DomainServiceAttribute>() && type.IsAssignableTo<IScoped>(),
                        order: 3
                    )
                    .Add(
                        add: new SingletonAttribute(),
                        when: type =>
                            type.Has<DomainServiceAttribute>() &&
                            !type.Has<TransientAttribute>() &&
                            !type.Has<ScopedAttribute>() &&
                            type.Properties.All(p => !p.IsPublic),
                        order: 4
                    );

            metadata
                .Method
                    .Add(
                        add: (method, adder) => adder.Add(method, new ApiMethodAttribute()),
                        when: method => method.Type.Has<SingletonAttribute>() && method.Overloads.Any(o => o.IsPublic)
                    );
        });

        configurator.ConfigureServiceCollection(services =>
        {
            var domainModel = configurator.Context.GetDomainModel();
            foreach (var type in domainModel.Types.Having<TransientAttribute>())
            {
                type.Apply(t =>
                {
                    services.AddTransientWithFactory(t);
                    type.Interfaces
                        .Where(i => i.IsDomainType)
                        .Apply(i => services.AddTransientWithFactory(i, t));
                });
            }

            foreach (var type in domainModel.Types.Having<ScopedAttribute>())
            {
                type.Apply(t =>
                {
                    services.AddScopedWithFactory(t);
                    type.Interfaces
                        .Where(i => i.IsDomainType)
                        .Apply(i => services.AddScopedWithFactory(i, t));
                });
            }

            foreach (var type in domainModel.Types.Having<SingletonAttribute>())
            {
                type.Apply(t =>
                {
                    services.AddSingleton(t);
                    type.Interfaces
                        .Where(i => i.IsDomainType)
                        .Apply(i => services.AddSingleton(i, t, forward: true));
                });
            }
        });

        configurator.ConfigureApiModel(api =>
        {
            api.References.AddRange(_domainAssemblies);

            var domainModel = configurator.Context.GetDomainModel();

            foreach (var type in domainModel.Types.Having<DomainServiceAttribute>())
            {
                if (type.FullName is null) { continue; }
                if (!type.Has<SingletonAttribute>()) { continue; } // TODO for now only singleton

                var controller = new ControllerModel(type.Name);
                foreach (var method in type.Methods.Having<ApiMethodAttribute>())
                {
                    var overload = method.Overloads.OrderByDescending(o => o.Parameters.Count).First();
                    if (overload.ReturnType is null) { continue; }

                    if (overload.Parameters.Count > 0) { continue; } // TODO for now only parameterless
                    if (!overload.ReturnType.IsAssignableTo(typeof(void)) &&
                        !overload.ReturnType.IsAssignableTo(typeof(Task))) { continue; } // TODO for now only void

                    controller.Actions.Add(
                        new(
                            Name: method.Name,
                            Method: HttpMethod.Post,
                            Route: $"generated/{type.Name}/{method.Name}",
                            Return: new(async: overload.ReturnType.IsAssignableTo(typeof(Task))),
                            Statements: new(
                                FindTarget: "target",
                                InvokeMethod: new(method.Name)
                            )
                        )
                        { Parameters = [new(ParameterModelFrom.Services, type.FullName, "target")] }
                    );
                }

                api.Controllers.Add(controller);
            }
        });

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
        });
    }
}
