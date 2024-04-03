using Do.Architecture;
using Do.Business.Attributes;
using Do.Business.Default.RestApiConventions;
using Do.Domain.Configuration;
using Do.Orm;
using Do.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Do.Business.Default;

public class DefaultBusinessFeature(List<Assembly> _domainAssemblies)
    : IFeature<BusinessConfigurator>
{
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
            builder.BindingFlags.Constructor = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            builder.BindingFlags.Method = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            builder.BindingFlags.Property = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

            builder.BuildLevels.Add(context => context.DomainTypesContain(context.Type), BuildLevels.Members);
            builder.BuildLevels.Add(context => context.Type.IsGenericType && context.DomainTypesContain(context.Type.GetGenericTypeDefinition()), BuildLevels.Members);
            builder.BuildLevels.Add(BuildLevels.Metadata);

            builder.Index.Type.Add<ServiceAttribute>();
            builder.Index.Type.Add<TransientAttribute>();
            builder.Index.Type.Add<ScopedAttribute>();
            builder.Index.Type.Add<SingletonAttribute>();

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
            builder.Metadata.Type.Add(new SingletonAttribute(),
               when: type =>
                   type.IsClass && !type.IsAbstract &&
                   type.TryGetMembers(out var members) &&
                   members.Has<ServiceAttribute>() &&
                   !members.Has<TransientAttribute>() &&
                   !members.Has<ScopedAttribute>() &&
                   members.Properties.All(p => !p.IsPublic),
               order: int.MaxValue
            );
            builder.Metadata.Type.Add(new TransientAttribute(),
                when: type =>
                    type.IsClass && !type.IsAbstract &&
                    type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    members.TryGetMethods("With", out var method) &&
                    method.Any(o =>
                        o.ReturnType is not null &&
                        (
                            o.ReturnType == type ||
                            (o.ReturnType.IsAssignableTo(typeof(Task<>)) && o.ReturnType.GetGenerics().GenericTypeArguments.First().Model == type)
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

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ApiInputAttribute>();
            builder.Index.Method.Add<ApiMethodAttribute>();

            builder.Metadata.Type.Add(new ApiInputAttribute(),
                when: type => type.IsAssignableTo(typeof(IParsable<>))
            );
            builder.Metadata.Type.Add(new ApiInputAttribute(),
                when: type => type.IsAssignableTo(typeof(string))
            );
            builder.Metadata.Type.Add(new ApiInputAttribute(),
                when: type => type.Has<EntityAttribute>(),
                order: int.MaxValue
            );
            builder.Metadata.Type.Add(new ApiInputAttribute(),
                when: type =>
                    type.IsAssignableTo(typeof(IEnumerable<>)) &&
                    type.IsGenericType && type.TryGetGenerics(out var generics) &&
                    generics.GenericTypeArguments.FirstOrDefault()?.Model.TryGetMetadata(out var genericArgMetadata) == true &&
                    genericArgMetadata.Has<ApiInputAttribute>(),
                order: int.MaxValue
            );
            builder.Metadata.Type.Add(new ApiInputAttribute(),
                when: type =>
                    type.IsArray && type.TryGetGenerics(out var generics) &&
                    generics.ElementType?.TryGetMetadata(out var elementMetadata) == true &&
                    elementMetadata.Has<ApiInputAttribute>(),
                order: int.MaxValue
            );

            builder.Metadata.Method.Add(new ApiMethodAttribute(),
                when: method => method.Overloads.Any(o => o.IsPublic && o.Parameters.All(p => p.ParameterType.TryGetMetadata(out var metadata) && metadata.Has<ApiInputAttribute>()))
            );
        });

        configurator.ConfigureApiModel(api =>
        {
            _domainAssemblies.ForEach(a => api.Reference.Add(a.GetName().FullName, a));

            var domainModel = configurator.Context.GetDomainModel();
            foreach (var type in domainModel.Types.Having<ServiceAttribute>())
            {
                if (type.FullName is null) { continue; }
                if (!(
                    type.GetMetadata().Has<SingletonAttribute>() || // for now only singleton
                    type.GetMetadata().Has<EntityAttribute>() // and entities
                )) { continue; }

                var controller = new ControllerModel(type);
                foreach (var method in type.GetMembers().Methods.Having<ApiMethodAttribute>())
                {
                    var overload = method.Overloads
                        .OrderByDescending(o => o.Parameters.Count) // overload with most parameters
                        .First(o => o.Parameters.All(p => p.ParameterType.TryGetMetadata(out var metadata) && metadata.Has<ApiInputAttribute>())); // with only api parameters
                    if (overload.ReturnType is null) { continue; }

                    controller.Action.Add(
                        method.Name,
                        new(
                            Id: method.Name,
                            Method: HttpMethod.Post,
                            Route: $"generated/{type.Name}/{method.Name}",
                            Return: new(overload.ReturnType),
                            FindTargetStatement: "target",
                            InvokedMethodName: method.Name
                        )
                        {
                            Parameters = [
                                new(type, ParameterModelFrom.Services, "target") { IsInvokeMethodParameter = false },
                                .. overload.Parameters.Select(p => new RestApi.Model.ParameterModel(p.ParameterType, ParameterModelFrom.Body, p.Name))
                            ]
                        }
                    );
                }

                api.Controller.Add(controller.Id, controller);
            }
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new LookupEntityByIdConvention(configurator.Context.GetDomainModel()));
            conventions.Add(new LookupEntitiesByIdsConvention(configurator.Context.GetDomainModel()));
            conventions.Add(new AutoHttpMethodConvention());
        });

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
        });
    }
}
