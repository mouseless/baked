using Do.Architecture;
using Do.Business.Attributes;
using Do.Business.Default.RestApiConventions;
using Do.Domain.Configuration;
using Do.Lifetime;
using Do.Orm;
using Do.RestApi.Model;
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
                    type.Name.EndsWith("Context")
            );
        });

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ApiServiceAttribute>();
            builder.Index.Type.Add<ApiInputAttribute>();
            builder.Index.Method.Add<ApiMethodAttribute>();

            builder.Metadata.Type.Add(new ApiServiceAttribute(),
                when: type =>
                  type.Has<ServiceAttribute>() &&
                  !type.IsGenericType &&
                  type.IsClass && !type.IsAbstract
            );
            builder.Metadata.Type.Add(new ApiInputAttribute(),
                when: type =>
                  type.IsEnum ||
                  type.Is<Uri>() ||
                  type.Is<object>() ||
                  type.IsAssignableTo(typeof(IParsable<>)) ||
                  type.IsAssignableTo(typeof(string))
            );
            builder.Metadata.Type.Add(new ApiInputAttribute(),
                when: type =>
                    type.IsAssignableTo(typeof(Nullable<>)) &&
                    type.GenericTypeArguments.FirstOrDefault()?.Model.TryGetMetadata(out var genericArgumentMetadata) == true &&
                    genericArgumentMetadata.Has<ApiInputAttribute>()
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
            api.References.AddRange(_domainAssemblies);

            var domainModel = configurator.Context.GetDomainModel();
            foreach (var type in domainModel.Types.Having<ApiServiceAttribute>())
            {
                if (type.FullName is null) { continue; }

                var controller = new ControllerModel(type);
                foreach (var method in type.GetMembers().Methods.Having<ApiMethodAttribute>())
                {
                    controller.AddAction(type, method);
                }

                api.Controller.Add(controller.Id, controller);
            }
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            conventions.Add(new EntityUnderEntitiesConvention());
            conventions.Add(new LookupEntityByIdConvention(domainModel, action => action.Id != "With"));
            conventions.Add(new LookupEntitiesByIdsConvention(domainModel));
            conventions.Add(new SingleByUniqueConvention(domainModel));

            conventions.Add(new AutoHttpMethodConvention());
            conventions.Add(new PublicWithIsPostResourceConvention());
            conventions.Add(new AddChildToChildrenConvention());
            conventions.Add(new GetChildrenToChildrenConvention());
            conventions.Add(new GetAndDeleteAcceptsOnlyQueryConvention());
            conventions.Add(new RemoveActionNameFromRouteConvention("With", "Delete", "Update", "By"));
            conventions.Add(new EnumDefaultValueConvention());
        });

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
        });
    }
}
