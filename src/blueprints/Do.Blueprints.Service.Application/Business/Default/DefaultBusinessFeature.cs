using Do.Architecture;
using Do.Domain.Configuration;
using Do.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Do.Business.Default;

public class DefaultBusinessFeature(List<Assembly> _domainAssemblies)
    : IFeature<BusinessConfigurator>
{
    const BindingFlags _bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

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
            options.BindingFlags.Constructor = _bindingFlags;
            options.BindingFlags.Method = _bindingFlags;
            options.BindingFlags.Property = _bindingFlags;

            options.BuildLevels.Add(context => context.DomainTypesContain(context.Type), BuildLevels.Members);
            options.BuildLevels.Add(context => context.Type.IsGenericType && context.DomainTypesContain(context.Type.GetGenericTypeDefinition()), BuildLevels.Members);
            options.BuildLevels.Add(type => !type.IsValueType, BuildLevels.Inheritance);
            options.BuildLevels.Add(type => type.IsGenericType, BuildLevels.Generics);
        });

        configurator.ConfigureDomainIndexers(indexers =>
        {
            indexers.AddAttributeIndexer<ServiceAttribute>();
            indexers.AddAttributeIndexer<TransientAttribute>();
            indexers.AddAttributeIndexer<ScopedAttribute>();
            indexers.AddAttributeIndexer<SingletonAttribute>();
            indexers.AddAttributeIndexer<ApiMethodAttribute>();
        });

        configurator.ConfigureDomainMetaData(metadata =>
        {
            metadata.Type.Add(
                add: new DataClassAttribute(),
                when: type => type.TryGetMembers(out var members) && members.MethodGroups.Contains("<Clone>$"), // if type is record
                order: int.MinValue
            );
            metadata.Type.Add(new ServiceAttribute(),
                when: type =>
                    !(
                        !type.IsPublic ||
                        type.IsValueType ||
                        type.IsGenericMethodParameter ||
                        type.IsGenericTypeParameter ||
                        type.IsGenericTypeDefinition ||
                        type.TryGetMetadata(out var metadata) && metadata.Has<DataClassAttribute>()
                    )
            );
            metadata.Type.Add(new TransientAttribute(),
                when: type =>
                    type.IsClass && !type.IsAbstract &&
                    type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    members.MethodGroups.TryGetValue("With", out var group) &&
                    group.Methods.Any(o =>
                        o.ReturnType is not null &&
                        (
                            o.ReturnType == type ||
                            (o.ReturnType.IsAssignableTo<Task>() && o.ReturnType.TryGetGenerics(out var returnTypeGenerics) && returnTypeGenerics.GenericTypeArguments.Contains(type))
                        )
                    )
            );
            metadata.Type.Add(new ScopedAttribute(),
                when: type =>
                    type.IsClass && !type.IsAbstract &&
                    type.TryGetMetadata(out var metadata) &&
                    metadata.Has<ServiceAttribute>() &&
                    type.IsAssignableTo<IScoped>()
            );
            metadata.Type.Add(new SingletonAttribute(),
                when: type =>
                    type.IsClass && !type.IsAbstract &&
                    type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    !members.Has<TransientAttribute>() &&
                    !members.Has<ScopedAttribute>() &&
                    members.Properties.All(p => !p.IsPublic)
            );

            metadata.MethodGroup.Add(
                apply: (group, adder) =>
                {
                    foreach (var method in group.Methods.Where(m => m.IsPublic))
                    {
                        adder.Add(method, new ApiMethodAttribute());
                    }
                },
                when: group => group.Methods.Any(m => m.IsPublic)
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
                    type.GetInheritance().Interfaces
                        .Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>())
                        .Apply(i => services.AddTransientWithFactory(i, t));
                });
            }

            foreach (var type in domainModel.Types.Having<ScopedAttribute>())
            {
                type.Apply(t =>
                {
                    services.AddScopedWithFactory(t);
                    type.GetInheritance().Interfaces
                        .Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>())
                        .Apply(i => services.AddScopedWithFactory(i, t));
                });
            }

            foreach (var type in domainModel.Types.Having<SingletonAttribute>())
            {
                type.Apply(t =>
                {
                    services.AddSingleton(t);
                    type.GetInheritance().Interfaces
                        .Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>())
                        .Apply(i => services.AddSingleton(i, t, forward: true));
                });
            }
        });

        configurator.ConfigureApiModel(api =>
        {
            api.References.AddRange(_domainAssemblies);

            var domainModel = configurator.Context.GetDomainModel();

            foreach (var type in domainModel.Types.Having<ServiceAttribute>())
            {
                if (type.FullName is null) { continue; }
                if (!type.GetMetadata().Has<SingletonAttribute>()) { continue; } // TODO for now only singleton

                var controller = new ControllerModel(type.Name);
                foreach (var group in type.GetMembers().MethodGroups.Having<ApiMethodAttribute>())
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
