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
        configurator.ConfigureDomainTypeCollection(types =>
        {
            foreach (var assembly in _domainAssemblies)
            {
                types.AddFromAssembly(assembly, except: type =>
                    (type.IsSealed && type.IsAbstract) || // if type is static
                    type.IsAssignableTo(typeof(Exception)) ||
                    type.IsAssignableTo(typeof(Attribute)) ||
                    type.IsAssignableTo(typeof(Delegate))
                );
            }
        });

        configurator.ConfigureDomainBuilderOptions(options =>
        {
            options.ReflectedType.ConstructorBindingFlags = _defaultMemberBindingFlags;
            options.ReflectedType.MethodBindingFlags = _defaultMemberBindingFlags;
            options.ReflectedType.PropertyBindingFlags = _defaultMemberBindingFlags;

            options.ReferencedType.ShouldSkipSetInheritance.When(t => t.IsValueType);
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
                        when: type => type.MethodGroups.Contains("<Clone>$"), // if type is record
                        order: int.MinValue
                    );
            metadata
                .Type
                    .Add(
                        add: new DomainServiceAttribute(),
                        when: type =>
                            !(
                                !type.IsPublic ||
                                type.IsInterface ||
                                type.IsAbstract ||
                                type.IsValueType ||
                                type.IsGenericMethodParameter ||
                                type.IsGenericTypeParameter ||
                                (type.ContainsGenericParameters && !type.GenericTypeArguments.Any()) ||
                                type.Has<DataClassAttribute>()
                            )
                    );
            metadata
                .Type
                    .Add(
                        add: new TransientAttribute(),
                        when: type =>
                            type.Has<DomainServiceAttribute>() &&
                            type.MethodGroups.TryGetValue("With", out var group) &&
                            group.Methods.Any(o =>
                                o.ReturnType == type ||
                                (o.ReturnType?.IsAssignableTo<Task>() == true && o.ReturnType?.GenericTypeArguments.Any(a => a == type) == true)
                            )
                    );
            metadata
                .Type
                    .Add(
                        add: new ScopedAttribute(),
                        when: type => type.Has<DomainServiceAttribute>() && type.IsAssignableTo<IScoped>()
                    );
            metadata
                .Type
                    .Add(
                        add: new SingletonAttribute(),
                        when: type =>
                            type.Has<DomainServiceAttribute>() &&
                            !type.Has<TransientAttribute>() &&
                            !type.Has<ScopedAttribute>() &&
                            type.Properties.All(p => !p.IsPublic)
                    );

            metadata
                .MethodGroup
                    .Add(
                        add: (group, adder) =>
                        {
                            foreach (var method in group.Methods.Where(m => m.IsPublic))
                            {
                                adder.Add(method, new ApiMethodAttribute());
                            }
                        },
                        when: group => group.ReflectedType.Has<SingletonAttribute>() && group.Methods.Any(m => m.IsPublic)
                    );
        });

        configurator.ConfigureServiceCollection(services =>
        {
            var domainModel = configurator.Context.GetDomainModel();
            foreach (var type in domainModel.ReflectedTypes.Having<TransientAttribute>())
            {
                type.Apply(t =>
                {
                    services.AddTransientWithFactory(t);
                    type.Interfaces
                        //.Where(i => i.IsDomainType) // will be filtered with new design
                        .Apply(i => services.AddTransientWithFactory(i, t));
                });
            }

            foreach (var type in domainModel.ReflectedTypes.Having<ScopedAttribute>())
            {
                type.Apply(t =>
                {
                    services.AddScopedWithFactory(t);
                    type.Interfaces
                        //.Where(i => i.IsDomainType) // will be filtered with new design
                        .Apply(i => services.AddScopedWithFactory(i, t));
                });
            }

            foreach (var type in domainModel.ReflectedTypes.Having<SingletonAttribute>())
            {
                type.Apply(t =>
                {
                    services.AddSingleton(t);
                    type.Interfaces
                        //.Where(i => i.IsDomainType) // will be filtered with new design
                        .Apply(i => services.AddSingleton(i, t, forward: true));
                });
            }
        });

        configurator.ConfigureApiModel(api =>
        {
            api.References.AddRange(_domainAssemblies);

            var domainModel = configurator.Context.GetDomainModel();

            foreach (var type in domainModel.ReflectedTypes.Having<DomainServiceAttribute>())
            {
                if (type.FullName is null) { continue; }
                if (!type.Has<SingletonAttribute>()) { continue; } // TODO for now only singleton

                var controller = new ControllerModel(type.Name);
                foreach (var group in type.MethodGroups.Having<ApiMethodAttribute>())
                {
                    var overload = group.Methods.OrderByDescending(o => o.Parameters.Count).First();
                    if (overload.ReturnType is null) { continue; }

                    if (overload.Parameters.Count > 0) { continue; } // TODO for now only parameterless
                    if (!overload.ReturnType.IsAssignableTo(typeof(void)) &&
                        !overload.ReturnType.IsAssignableTo(typeof(Task))) { continue; } // TODO for now only void

                    controller.Actions.Add(
                        new(
                            Name: group.Name,
                            Method: HttpMethod.Post,
                            Route: $"generated/{type.Name}/{group.Name}",
                            Return: new(async: overload.ReturnType.IsAssignableTo(typeof(Task))),
                            Statements: new(
                                FindTarget: "target",
                                InvokeMethod: new(group.Name)
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
