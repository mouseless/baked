using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.RestApi;
using Baked.RestApi.Conventions;
using Baked.RestApi.Model;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Baked.Business.DomainAssemblies;

public class DomainAssembliesBusinessFeature(List<Assembly> _assemblies, Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel> _defaultOverloadSelector)
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

        configurator.ConfigureConfigurationBuilder(configuration =>
        {
            configuration.AddJson($$"""
            {
              "Logging": {
                "LogLevel": {
                  "Default": "{{(configurator.IsProduction() ? "Error" : "Information")}}",
                  "Microsoft.AspNetCore": "Error",
                  "Microsoft.Hosting.Lifetime": "Information"
                }
              }
            }
            """);
        });

        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.BindingFlags.Constructor = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            builder.BindingFlags.Method = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            builder.BindingFlags.Property = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

            builder.DefaultOverloadSelector = _defaultOverloadSelector;

            builder.BuildLevels.Add(context => context.DomainTypesContain(context.Type), BuildLevels.Members);
            builder.BuildLevels.Add(context => context.Type.IsGenericType && context.DomainTypesContain(context.Type.GetGenericTypeDefinition()), BuildLevels.Members);
            builder.BuildLevels.Add(BuildLevels.Metadata);

            builder.Index.Type.Add<ServiceAttribute>();
            builder.Index.Type.Add<CasterAttribute>();

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
            builder.Conventions.AddMethodMetadata(new ExternalAttribute(),
                when: c =>
                    c.Method.DefaultOverload.DeclaringType is not null &&
                    c.Method.DefaultOverload.DeclaringType.TryGetMetadata(out var metadata) &&
                    !metadata.Has<ServiceAttribute>()
            );
            builder.Conventions.AddMethodMetadata(new ExternalAttribute(),
                when: c =>
                    c.Method.DefaultOverload.BaseDefinition is not null &&
                    c.Method.DefaultOverload.BaseDefinition.DeclaringType is not null &&
                    c.Method.DefaultOverload.BaseDefinition.DeclaringType.TryGetMetadata(out var metadata) &&
                    !metadata.Has<ServiceAttribute>()
            );
            builder.Conventions.AddTypeMetadata(new CasterAttribute(),
                when: c => c.Type.IsClass && !c.Type.IsAbstract && c.Type.IsAssignableTo(typeof(ICasts<,>))
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
                  members.Methods.Any(m => m.DefaultOverload.IsPublicInstanceWithNoSpecialName())
            );
            builder.Conventions.AddMethodMetadata(new ApiMethodAttribute(),
                when: c =>
                    !c.Method.Has<ExternalAttribute>() &&
                    !c.Method.Has<InitializerAttribute>() &&
                    c.Method.DefaultOverload.IsPublicInstanceWithNoSpecialName() &&
                    c.Method.DefaultOverload.AllParametersAreApiInput(),
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

                var controller = new ControllerModel(type) { ClassName = type.CSharpFriendlyFullName.Split('.').Skip(1).Join('_') };
                foreach (var method in type.GetMembers().Methods.Having<ApiMethodAttribute>())
                {
                    controller.AddAction(type, method);
                }

                if (!controller.Action.Any()) { continue; }

                api.Controller.Add(controller.Id, controller);
            }
        });

        configurator.ConfigureServiceProvider(sp =>
        {
            Caster.SetServiceProvider(sp);
            var domainModel = configurator.Context.GetDomainModel();
            foreach (var type in domainModel.Types.Having<CasterAttribute>())
            {
                foreach (var @interface in type.GetInheritance().Interfaces.Where(i => i.Model.IsGenericType && !i.Model.IsGenericTypeDefinition && i.Model.IsAssignableTo(typeof(ICasts<,>))))
                {
                    type.Apply(t => @interface.Apply(i =>
                    {
                        Caster.Add(i.GenericTypeArguments[0], i.GenericTypeArguments[1], sp => sp.GetRequiredServiceUsingRequestServices(t));
                    }));
                }
            }
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new AutoHttpMethodConvention([
                (Regexes.StartsWithGet(), HttpMethod.Get),
                (Regexes.IsUpdateChangeOrSet(), HttpMethod.Put),
                (Regexes.StartsWithUpdateChangeOrSet(), HttpMethod.Patch),
                (Regexes.StartsWithDeleteRemoveOrClear(), HttpMethod.Delete)
            ]));
            conventions.Add(new GetAndDeleteAcceptsOnlyQueryConvention());
            conventions.Add(new RemoveFromRouteConvention(["Get"]));
            conventions.Add(new RemoveFromRouteConvention(["Update", "Change", "Set"]));
            conventions.Add(new RemoveFromRouteConvention(["Delete", "Remove", "Clear"]));
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.CustomSchemaIds(t =>
            {
                string[] splitedNamespace = t.Namespace?.Split(".") ?? [];
                string name = t.IsNested && t.FullName is not null
                    ? t.FullName.Replace($"{t.Namespace}.", string.Empty).Replace("+", "_")
                    : t.Name;

                var result = splitedNamespace.Length > 1
                    ? $"{splitedNamespace.Skip(1).Join('_')}_{name}"
                    : name;

                return result.Replace("_", "--").Kebaberize();
            });

            swaggerGenOptions.OrderActionsBy(apiDescription =>
            {
                var methodOrder =
                    apiDescription.HttpMethod == "POST" ? 0 :
                    apiDescription.HttpMethod == "GET" ? 1 :
                    apiDescription.HttpMethod == "PUT" ? 2 :
                    apiDescription.HttpMethod == "PATCH" ? 3 :
                    4;

                return $"{apiDescription.ActionDescriptor.AttributeRouteInfo?.Template}_{methodOrder}";
            });
        });
    }
}