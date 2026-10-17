using Baked.Architecture;
using Baked.Business;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Baked;

public static class DomainAssembliesBusinessFeature
{
    public static IFeature<BusinessFeature.Configurator> DomainAssemblies(this BusinessFeature.Configurator x,
        Assembly assembly,
        string? baseNamespace = default,
        Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel>? defaultOverloadSelector = default,
        bool addEmbeddedFileProviders = true,
        Func<TypeModel, bool>? setNamespaceWhen = default
    ) => x.DomainAssemblies([assembly],
        baseNamespace: baseNamespace is not null ? _ => baseNamespace : null,
        defaultOverloadSelector: defaultOverloadSelector,
        addEmbeddedFileProviders: addEmbeddedFileProviders,
        setNamespaceWhen: setNamespaceWhen
    );

    public static IFeature<BusinessFeature.Configurator> DomainAssemblies(this BusinessFeature.Configurator x,
        IEnumerable<Assembly> assemblies,
        Func<Assembly, string>? baseNamespace = default,
        Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel>? defaultOverloadSelector = default,
        bool addEmbeddedFileProviders = true,
        Func<TypeModel, bool>? setNamespaceWhen = default
    )
    {
        baseNamespace ??= (a => a.GetName().Name ?? string.Empty);

        return x.DomainAssemblies(assemblies.Select(a => (a, baseNamespace(a))),
            defaultOverloadSelector: defaultOverloadSelector,
            addEmbeddedFileProviders: addEmbeddedFileProviders,
            setNamespaceWhen: setNamespaceWhen
        );
    }

    public static IFeature<BusinessFeature.Configurator> DomainAssemblies(this BusinessFeature.Configurator x,
        IEnumerable<(Assembly assembly, string baseNamespace)> assemblyDescriptors,
        Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel>? defaultOverloadSelector = default,
        bool addEmbeddedFileProviders = true,
        Func<TypeModel, bool>? setNamespaceWhen = default
    ) => x.Define((
        defaultOverloadSelector ?? (overloads =>
            overloads.FirstPublicInstanceWithMostParametersOrDefault() ??
            overloads.FirstNonPublicInstanceWithMostParametersOrDefault() ??
            overloads.FirstPublicStaticWithMostParametersOrDefault() ??
            overloads.FirstNonPublicStaticWithMostParametersOrDefault() ??
            overloads.FirstWithMostParametersOrDefault() ??
            throw new($"Method without an overload should not exist")
        ),
        setNamespaceWhen ?? (t => true),
        assemblyDescriptors.ToDictionary(kvp => kvp.assembly, kvp => kvp.baseNamespace)
    ), (configurator, args) =>
    {
        var (defaultOverloadSelector, setNamespaceWhen, baseNamespaces) = args;

        configurator.Domain.ConfigureDomainTypeCollection(types =>
        {
            foreach (var (assembly, _) in assemblyDescriptors)
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

        configurator.Runtime.ConfigureConfigurationBuilder(configuration =>
        {
            configuration.AddJsonAsDefault($$"""
            {
              "Logging": {
                "LogLevel": {
                  "Default": "{{(configurator.IsProduction ? "Error" : "Information")}}",
                  "Microsoft.AspNetCore": "Error",
                  "Microsoft.Hosting.Lifetime": "Information"
                }
              }
            }
            """);
        });

        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.BindingFlags.Constructor = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            builder.BindingFlags.Method = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            builder.BindingFlags.Property = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            builder.DefaultOverloadSelector = defaultOverloadSelector;

            builder.BuildLevels.Add(context => context.DomainTypesContain(context.Type), BuildLevels.Members);
            builder.BuildLevels.Add(context => context.Type.IsGenericType && context.DomainTypesContain(context.Type.GetGenericTypeDefinition()), BuildLevels.Members);
            builder.BuildLevels.Add(BuildLevels.Metadata);

            builder.ConventionOrderMatrix.Bases.Add("Business");
            builder.ConventionOrderMatrix.Levels.Add("Infra");
            builder.ConventionOrderMatrix.Levels.Add("Defaults");
            builder.ConventionOrderMatrix.Levels.Add("Override");
            builder.ConventionOrderMatrix.Extensions.Add("Add");
            builder.ConventionOrderMatrix.Extensions.Add("Configure");

            builder.ConventionOrderMatrix.FallbackBase = _ => "Business";
            builder.ConventionOrderMatrix.FallbackLevel = _ => "Defaults";
            builder.ConventionOrderMatrix.FallbackExtension = convention => convention.BeforeBuildingIndexes ? "Add" : "Configure";

            builder.DefaultConventionLevel = "Business.Defaults.Configure";

            builder.Index.Type.Add<Service>();
            builder.Index.Type.Add<Query>();
            builder.Index.Type.Add<Severities>();
            builder.Index.Method.Add<Initializer>();
            builder.Index.Property.Add<IdProperty>();
            builder.Index.Property.Add<Label>();
            builder.Index.Property.Add<Status>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: _ => true,
                attribute: () => new Group(),
                order: Order.At.Global.Min
            );
            conventions.SetPropertyAttribute(
                when: _ => true,
                attribute: () => new Group(),
                order: Order.At.Global.Min
            );
            conventions.SetMethodAttribute(
                when: _ => true,
                attribute: () => new Group(),
                order: Order.At.Global.Min
            );
            conventions.SetParameterAttribute(
                when: _ => true,
                attribute: () => new Group(),
                order: Order.At.Global.Min
            );

            conventions.SetTypeAttribute(
                attribute: context =>
                {
                    var @namespace = context.Type.Namespace ?? string.Empty;
                    context.Type.Apply(t =>
                    {
                        if (!baseNamespaces.TryGetValue(t.Assembly, out var baseNamespace)) { return; }

                        @namespace =
                            @namespace == baseNamespace ? string.Empty :
                            @namespace.StartsWith(baseNamespace) ? @namespace[(baseNamespace.Length + 1)..] :
                            @namespace;
                    });

                    return new Namespace(@namespace);
                },
                when: c => setNamespaceWhen(c.Type),
                order: Order.At.Infra
            );
            conventions.SetTypeAttribute(
                attribute: () => new Service(),
                when: c =>
                    c.Type.IsPublic &&
                    !c.Type.IsValueType &&
                    !c.Type.IsGenericMethodParameter &&
                    !c.Type.IsGenericTypeParameter &&
                    !c.Type.IsGenericTypeDefinition &&
                    !c.Type.IsAssignableTo<IEnumerable>() &&
                    c.Type.TryGetMembers(out var members) &&
                    !members.Methods.Contains("<Clone>$"), // if type is record
                order: Order.At.Infra
            );

            conventions.SetMethodAttribute(
                attribute: () => new External(),
                when: c =>
                    c.Method.DefaultOverload.DeclaringType is not null &&
                    c.Method.DefaultOverload.DeclaringType.TryGetMetadata(out var metadata) &&
                    !metadata.Has<Service>(),
                order: Order.At.Infra
            );

            conventions.SetMethodAttribute(
                attribute: () => new External(),
                when: c =>
                    c.Method.DefaultOverload.BaseDefinition is not null &&
                    c.Method.DefaultOverload.BaseDefinition.DeclaringType is not null &&
                    c.Method.DefaultOverload.BaseDefinition.DeclaringType.TryGetMetadata(out var metadata) &&
                    !metadata.Has<Service>(),
                order: Order.At.Infra
            );
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<Validate>();

            foreach (var (assembly, baseNamespace) in assemblyDescriptors)
            {
                if (addEmbeddedFileProviders)
                {
                    services.AddFileProvider(new EmbeddedFileProvider(assembly, baseNamespace));
                }
            }
        });

        configurator.RestApi.ConfigureApiModel(api =>
        {
            api.References.AddRange(assemblyDescriptors.Select(a => a.assembly));
        });

        configurator.Domain.ConfigureDomainServiceCollection(services =>
        {
            services.References.AddRange(assemblyDescriptors.Select(ad => ad.assembly));
            services.Usings.AddRange([
                "Baked.Business",
                "Baked.Runtime",
                "Microsoft.Extensions.DependencyInjection"
            ]);
        });

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            foreach (var (assembly, _) in assemblyDescriptors)
            {
                var xmlPath = XmlComments.GetPath(assembly);
                if (xmlPath is null) { continue; }

                swaggerGenOptions.IncludeXmlComments(xmlPath);
            }
        });
    });

    extension(IEnumerable<MethodOverloadModel> overloads)
    {
        public MethodOverloadModel? FirstPublicInstanceWithMostParametersOrDefault() =>
            overloads
                .Where(o => o.IsPublic && !o.IsStatic)
                .OrderByDescending(o => o.Parameters.Count)
                .FirstOrDefault();

        public MethodOverloadModel? FirstNonPublicInstanceWithMostParametersOrDefault() =>
            overloads
                .Where(o => !o.IsPublic && !o.IsStatic)
                .OrderByDescending(o => o.Parameters.Count)
                .FirstOrDefault();

        public MethodOverloadModel? FirstPublicStaticWithMostParametersOrDefault() =>
            overloads
                .Where(o => o.IsPublic && o.IsStatic)
                .OrderByDescending(o => o.Parameters.Count)
                .FirstOrDefault();

        public MethodOverloadModel? FirstNonPublicStaticWithMostParametersOrDefault() =>
            overloads
                .Where(o => !o.IsPublic && o.IsStatic)
                .OrderByDescending(o => o.Parameters.Count)
                .FirstOrDefault();

        public MethodOverloadModel? FirstWithMostParametersOrDefault() =>
            overloads
                .OrderByDescending(o => o.Parameters.Count)
                .FirstOrDefault();
    }
}