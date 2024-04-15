using Do.Architecture;
using Do.Domain.Configuration;
using Do.Domain.Model;
using Do.RestApi.Conventions;
using Do.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Do.Business.DomainAssemblies;

public class DomainAssembliesBusinessFeature(List<Assembly> _assemblies, Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel> _overloadSelector)
    : IFeature<BusinessConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainTypeCollection(types =>
        {
            foreach (var assembly in _assemblies)
            {
                types.AddFromAssembly(assembly,
                    except: type =>
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

            builder.Conventions.AddTypeMetadata(new ServiceAttribute(),
                when: type =>
                    type.IsPublic &&
                    !type.IsValueType &&
                    !type.IsGenericMethodParameter &&
                    !type.IsGenericTypeParameter &&
                    !type.IsGenericTypeDefinition &&
                    !type.IsAssignableTo<IEnumerable>() &&
                    type.TryGetMembers(out var members) &&
                    !members.Methods.Contains("<Clone>$") // if type is record
            );
        });

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ApiServiceAttribute>();
            builder.Index.Type.Add<ApiInputAttribute>();
            builder.Index.Method.Add<ApiMethodAttribute>();

            builder.Conventions.AddTypeMetadata(new ApiServiceAttribute(),
                when: type =>
                  type.Has<ServiceAttribute>() &&
                  type.IsClass &&
                  !type.IsAbstract &&
                  !type.IsGenericType &&
                  type.TryGetMembers(out var members) &&
                  members.Methods.Any()
            );
            builder.Conventions.AddMethodMetadata(new ApiMethodAttribute(),
                when: method =>
                    method.Overloads.Any(o => o.IsPublic && o.AllParametersAreApiInput()),
                order: int.MaxValue
            );
        });

        configurator.ConfigureApiModel(api =>
        {
            api.References.AddRange(_assemblies);

            var domainModel = configurator.Context.GetDomainModel();
            foreach (var type in domainModel.Types.Having<ApiServiceAttribute>())
            {
                if (type.FullName is null) { continue; }

                var controller = new ControllerModel(type);
                foreach (var method in type.GetMembers().Methods.Having<ApiMethodAttribute>())
                {
                    var overload = _overloadSelector(method.Overloads.Where(o => o.IsPublic && o.AllParametersAreApiInput()));

                    controller.AddAction(type, method.Name, overload);
                }

                api.Controller.Add(controller.Id, controller);
            }
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new AutoHttpMethodConvention());
            conventions.Add(new GetAndDeleteAcceptsOnlyQueryConvention());
            conventions.Add(new RemoveActionNameFromRouteConvention(["Delete", "Update"]));
            conventions.Add(new RemovePrefixFromRouteConvention(["Get"]));
            conventions.Add(new AddResourceConvention());
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.CustomSchemaIds(t =>
            {
                string[] splitedNamespace = t.Namespace?.Split(".") ?? [];
                string name = t.IsNested && t.FullName is not null
                    ? t.FullName.Replace($"{t.Namespace}.", string.Empty).Replace("+", ".")
                    : t.Name;

                return splitedNamespace.Length > 1
                    ? $"{string.Join('.', splitedNamespace.Skip(1))}.{name}"
                    : name;
            });
        });
    }
}