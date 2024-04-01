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

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.BindingFlags.Constructor = _bindingFlags;
            builder.BindingFlags.Method = _bindingFlags;
            builder.BindingFlags.Property = _bindingFlags;

            builder.BuildLevels.Add(context => context.DomainTypesContain(context.Type), BuildLevels.Members);
            builder.BuildLevels.Add(context => context.Type.IsGenericType && context.DomainTypesContain(context.Type.GetGenericTypeDefinition()), BuildLevels.Members);
            builder.BuildLevels.Add(type => !type.IsValueType, BuildLevels.Inheritance);
            builder.BuildLevels.Add(type => type.IsGenericType, BuildLevels.Generics);

            builder.Index.Type.Add<ServiceAttribute>();
            builder.Index.Type.Add<TransientAttribute>();
            builder.Index.Type.Add<ScopedAttribute>();
            builder.Index.Type.Add<SingletonAttribute>();
            builder.Index.Method.Add<ApiMethodAttribute>();

            builder.Metadata.Type.Add(new DataClassAttribute(),
                when: type =>
                    type.TryGetMembers(out var members) &&
                    members.Methods.Contains("<Clone>$"), // if type is record
                order: int.MinValue
            );
            builder.Metadata.Type.Add(new ServiceAttribute(),
                when: type =>
                    type.IsPublic &&
                    !type.IsAssignableTo<IEnumerable>() &&
                    !type.IsValueType &&
                    !type.IsGenericMethodParameter &&
                    !type.IsGenericTypeParameter &&
                    !type.IsGenericTypeDefinition &&
                    type.TryGetMembers(out var members) &&
                    !members.Has<DataClassAttribute>()
            );
            builder.Metadata.Type.Add(new TransientAttribute(),
                when: type =>
                    type.IsClass && !type.IsAbstract &&
                    type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    members.Methods.TryGetGroup("With", out var methods) &&
                    methods.Any(o =>
                        o.ReturnType is not null &&
                        (
                            o.ReturnType == type ||
                            (o.ReturnType.IsAssignableTo<Task>() && o.ReturnType.TryGetGenerics(out var returnTypeGenerics) && returnTypeGenerics.GenericTypeArguments.Contains(type))
                        )
                    )
            );
            builder.Metadata.Type.Add(new ScopedAttribute(),
                when: type =>
                    type.IsClass && !type.IsAbstract &&
                    type.TryGetMetadata(out var metadata) &&
                    metadata.Has<ServiceAttribute>() &&
                    type.IsAssignableTo<IScoped>()
            );
            builder.Metadata.Type.Add(new SingletonAttribute(),
                when: type =>
                    type.IsClass && !type.IsAbstract &&
                    type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    !members.Has<TransientAttribute>() &&
                    !members.Has<ScopedAttribute>() &&
                    members.Properties.All(p => !p.IsPublic)
            );

            builder.Metadata.Method.Add(new ApiMethodAttribute(),
                when: method => method.IsPublic
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
                foreach (var method in type.GetMembers().Methods.Having<ApiMethodAttribute>())
                {
                    if (method.ReturnType is null) { continue; }

                    if (method.Parameters.Count > 0) { continue; } // TODO for now only parameterless
                    if (!method.ReturnType.IsAssignableTo(typeof(void)) &&
                        !method.ReturnType.IsAssignableTo(typeof(Task))) { continue; } // TODO for now only void

                    controller.Actions.Add(
                        new(
                            Name: method.Name,
                            Method: HttpMethod.Post,
                            Route: $"generated/{type.Name}/{method.Name}",
                            Return: new(async: method.ReturnType.IsAssignableTo(typeof(Task))),
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
