using Do.Architecture;
using Do.Domain.Configuration;
using Do.Domain.Model;
using Do.RestApi;
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
                when: c =>
                    c.Type.IsPublic &&
                    !c.Type.IsValueType &&
                    !c.Type.IsGenericMethodParameter &&
                    !c.Type.IsGenericTypeParameter &&
                    !c.Type.IsGenericTypeDefinition &&
                    !c.Type.IsAssignableTo<IEnumerable>() &&
                    c.Type.TryGetMembers(out var members) &&
                    !members.Methods.Contains("<Clone>$") // if type is record
            );
        });

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ApiServiceAttribute>();
            builder.Index.Type.Add<ApiInputAttribute>();

            builder.Index.Method.Add<InitializerAttribute>();
            builder.Index.Method.Add<ApiMethodAttribute>();

            builder.Conventions.AddTypeMetadata(new ApiServiceAttribute(),
                when: c =>
                  c.Type.Has<ServiceAttribute>() &&
                  c.Type.IsClass &&
                  !c.Type.IsAbstract &&
                  !c.Type.IsGenericType &&
                  c.Type.TryGetMembers(out var members) &&
                  members.Methods.Any()
            );
            builder.Conventions.AddMethodMetadata(new ApiMethodAttribute(),
                when: c =>
                    !c.Method.Has<InitializerAttribute>() &&
                    c.Method.Overloads.Any(o => o.IsPublic && o.AllParametersAreApiInput()),
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

                    controller.AddAction(type, method, overload);
                }

                api.Controller.Add(controller.Id, controller);
            }
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new AutoHttpMethodConvention([
                (Regexes.StartsWithGet(), HttpMethod.Get),
                (Regexes.IsUpdateChangeOrSet(), HttpMethod.Put),
                (Regexes.StartsWithUpdateChangeOrSet(), HttpMethod.Patch),
                (Regexes.StartsWithDeleteOrRemove(), HttpMethod.Delete)
            ]));
            conventions.Add(new GetAndDeleteAcceptsOnlyQueryConvention());
            conventions.Add(new RemovePrefixFromRouteConvention(["Get"]));
            conventions.Add(new RemoveActionNameFromRouteConvention(["Update", "Change", "Set"]));
            conventions.Add(new RemoveActionNameFromRouteConvention(["Delete", "Remove"]));
            conventions.Add(new RemovePrefixFromRouteConvention(["Add"], _pluralize: true));
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