using Baked.Architecture;
using Baked.Binding;
using Baked.Business;
using Baked.CodingStyle.ResourceViaIdInitializer;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.RestApi.Model;
using Humanizer;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class ResourceViaIdInitializerCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> ResourceViaIdInitializer(this CodingStyleFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Type.Add<Resource>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<Service>() &&
                    members.Has<Transient>() &&
                    TryFindIdProperty(members, out var idProperty) &&
                    members.Methods.Any(m =>
                        m.Has<Initializer>() &&
                        m.DefaultOverload.Parameters.Count == 1 &&
                        m.DefaultOverload.Parameters.All(p =>
                            p.Name == idProperty.Name.Camelize() &&
                            p.ParameterType == idProperty.PropertyType
                        )
                    ),
                apply: (c, set) =>
                {
                    set(c.Type, new Resource());
                    set(c.Type, new Bindable());
                    set(c.Type, new Locatable());
                },
                order: Order.At.Infra + 10
            );
            conventions.EditTypeAttribute<Locatable>(
                when: c => c.Type.Has<Resource>(),
                attribute: (locatable, c) =>
                {
                    if (!c.Type.TryGetMembers(out var members)) { return; }

                    var initializer =
                        members.Methods.FirstOrDefault(m => m.Has<Initializer>()) ??
                        throw DiagnosticCode.MethodWithAttribute.Exception(
                            $"`{c.Type.Name}` should have had method with `InitializerAttribute`."
                        );

                    locatable.IsAsync = initializer.DefaultOverload.ReturnType.IsAssignableTo<Task>();
                },
                order: Order.At.Infra + 10
            );
            conventions.SetMethodAttribute(
                when: c =>
                    c.Type.Has<Resource>() &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.Any(p => p.IsPublic) &&
                    c.Method.Has<Initializer>() &&
                    c.Method.DefaultOverload.IsPublic,
                attribute: c => new ApiAction(),
                order: Order.At.Infra + 20
            );

            conventions.Add(new ResourceUnderPluralGroupConvention(), order: Order.At.Infra);
            conventions.Add(new ResourceInitializerIsGetConvention(), order: Order.At.Infra + 10);
        });

        configurator.Buildtime.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(ResourceViaIdInitializerCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<CodingStyleFeature.Configurator>()
                        .AddCodes(new LocatorTemplate(domain)),
                    usings: [.. LocatorTemplate.GlobalUsings]
                );
            });
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            configurator.Buildtime.UsingGeneratedContext(context =>
            {
                services.AddFromAssembly(context.Assemblies[nameof(ResourceViaIdInitializerCodingStyleFeature)]);
            });
        });
    });

    static bool TryFindIdProperty(TypeModelMembers members, [NotNullWhen(true)] out PropertyModel? property)
    {
        property = members.Properties.FirstOrDefault(p => p.CustomAttributes.Contains<IdProperty>());

        return property is not null;
    }

    extension(ApiAction action)
    {
        public ApiParameter AddFactoryAsService(TypeModel transientType)
        {
            var parameter =
                new ApiParameter($"new{transientType.Name.Pascalize()}", $"Func<{transientType.CSharpFriendlyFullName}>", ParameterModelFrom.Services)
                {
                    IsInvokeMethodParameter = false,
                };

            action.Parameter[parameter.Name] = parameter;

            return parameter;
        }
    }

    extension(TypeModel type)
    {
        public string BuildInitializerById(string valueExpression,
            string? notNullValueExpression = default,
            bool nullable = false
        )
        {
            notNullValueExpression ??= valueExpression;

            var initializer = type.GetMembers().Methods.Having<Initializer>().Single();
            var initializerById = $"new{type.Name.Pascalize()}().{initializer.Name}({notNullValueExpression})";
            if (initializer.DefaultOverload.ReturnType.IsAssignableTo<Task>())
            {
                initializerById = $"(await {initializerById})";
            }

            if (nullable)
            {
                initializerById = $"({valueExpression} != null ? {initializerById} : null)";
            }

            return initializerById;
        }

        public string BuildInitializerByIds(string valueExpression,
            bool isArray = default
        )
        {
            var initializer = type.GetMembers().Methods.Having<Initializer>().Single();
            var byIds = $"{valueExpression}.Select(id => new{type.Name.Pascalize()}().{initializer.Name}(id))";
            if (initializer.DefaultOverload.ReturnType.IsAssignableTo<Task>())
            {
                byIds = $"(await Task.WhenAll({byIds}))";
            }

            return isArray
                ? $"{byIds}.ToArray()"
                : $"{byIds}.ToList()";
        }
    }
}