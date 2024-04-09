using Do.Architecture;
using Do.Business.Attributes;
using Do.Business.Default.RestApiConventions;
using Do.Domain.Configuration;
using Do.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;
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

            builder.Conventions.AddType(new DataAttribute(),
                when: type =>
                    type.TryGetMembers(out var members) &&
                    members.Methods.Contains("<Clone>$"), // if type is record
                order: int.MinValue
            );
            builder.Conventions.AddType(new ServiceAttribute(),
                when: type =>
                    type.IsPublic &&
                    !type.IsAssignableTo<IEnumerable>() &&
                    !type.IsValueType &&
                    !type.IsGenericMethodParameter &&
                    !type.IsGenericTypeParameter &&
                    !type.IsGenericTypeDefinition &&
                    type.TryGetMembers(out var members) &&
                    !members.Has<DataAttribute>()
            );
        });

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ApiServiceAttribute>();
            builder.Index.Type.Add<ApiInputAttribute>();
            builder.Index.Method.Add<ApiMethodAttribute>();

            builder.Conventions.AddType(new ApiServiceAttribute(),
                when: type =>
                  type.Has<ServiceAttribute>() &&
                  type.IsClass &&
                  !type.IsAbstract &&
                  !type.IsGenericType &&
                  type.TryGetMembers(out var members) &&
                  members.Methods.Any()
            );
            builder.Conventions.AddMethod(new ApiMethodAttribute(),
                when: method => method.Overloads.Any(o => o.IsPublic && o.Parameters.All(p => p.ParameterType.TryGetMetadata(out var metadata) && metadata.Has<ApiInputAttribute>())),
                order: int.MaxValue
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
            conventions.Add(new AutoHttpMethodConvention());
            conventions.Add(new GetAndDeleteAcceptsOnlyQueryConvention());
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.CustomSchemaIds(t =>
            {
                string[] splitedNamespace = t.Namespace?.Split(".") ?? [];
                string name = t.IsNested && t.FullName is not null
                    ? t.FullName.Replace($"{t.Namespace}.", string.Empty).Replace("Controller", string.Empty).Replace("+", ".")
                    : t.Name;

                return splitedNamespace.Length > 1
                    ? $"{string.Join('.', splitedNamespace.Skip(1))}.{name}"
                    : name;
            });
        });
    }
}