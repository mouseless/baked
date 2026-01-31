using Baked.Architecture;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Baked.Business.DomainAssemblies;

public class DomainAssembliesBusinessFeature(
    IEnumerable<(Assembly assembly, string baseNamespace)> _assemblyDescriptors,
    Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel> _defaultOverloadSelector,
    bool _addEmbeddedFileProviders,
    Func<TypeModel, bool> setNamespaceWhen
) : IFeature<BusinessConfigurator>
{

    Dictionary<Assembly, string> BaseNamespaces { get; } = _assemblyDescriptors.ToDictionary(kvp => kvp.assembly, kvp => kvp.baseNamespace);

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainTypeCollection(types =>
        {
            foreach (var (assembly, _) in _assemblyDescriptors)
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
            configuration.AddJsonAsDefault($$"""
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
            builder.BindingFlags.Property = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            builder.DefaultOverloadSelector = _defaultOverloadSelector;

            builder.BuildLevels.Add(context => context.DomainTypesContain(context.Type), BuildLevels.Members);
            builder.BuildLevels.Add(context => context.Type.IsGenericType && context.DomainTypesContain(context.Type.GetGenericTypeDefinition()), BuildLevels.Members);
            builder.BuildLevels.Add(BuildLevels.Metadata);

            builder.Index.Type.Add<ServiceAttribute>();
            builder.Index.Type.Add<CasterAttribute>();
            builder.Index.Method.Add<InitializerAttribute>();
            builder.Index.Property.Add<IdAttribute>();
            builder.Index.Property.Add<LabelAttribute>();

            builder.Conventions.SetTypeAttribute(
                attribute: context =>
                {
                    var @namespace = context.Type.Namespace ?? string.Empty;
                    context.Type.Apply(t =>
                    {
                        if (!BaseNamespaces.TryGetValue(t.Assembly, out var baseNamespace)) { return; }

                        @namespace =
                            @namespace == baseNamespace ? string.Empty :
                            @namespace.StartsWith(baseNamespace) ? @namespace[(baseNamespace.Length + 1)..] :
                            @namespace;
                    });

                    return new NamespaceAttribute(@namespace);
                },
                when: c => setNamespaceWhen(c.Type)
            );
            builder.Conventions.SetTypeAttribute(
                attribute: () => new ServiceAttribute(),
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
            builder.Conventions.SetMethodAttribute(
                attribute: () => new ExternalAttribute(),
                when: c =>
                    c.Method.DefaultOverload.DeclaringType is not null &&
                    c.Method.DefaultOverload.DeclaringType.TryGetMetadata(out var metadata) &&
                    !metadata.Has<ServiceAttribute>()
            );
            builder.Conventions.SetMethodAttribute(
                attribute: () => new ExternalAttribute(),
                when: c =>
                    c.Method.DefaultOverload.BaseDefinition is not null &&
                    c.Method.DefaultOverload.BaseDefinition.DeclaringType is not null &&
                    c.Method.DefaultOverload.BaseDefinition.DeclaringType.TryGetMetadata(out var metadata) &&
                    !metadata.Has<ServiceAttribute>()
            );
            builder.Conventions.SetTypeAttribute(
                attribute: () => new CasterAttribute(),
                when: c => c.Type.IsClass && !c.Type.IsAbstract && c.Type.IsAssignableTo(typeof(ICasts<,>))
            );
        });

        configurator.ConfigureServiceCollection(services =>
        {
            foreach (var (assembly, baseNamespace) in _assemblyDescriptors)
            {
                if (_addEmbeddedFileProviders)
                {
                    services.AddFileProvider(new EmbeddedFileProvider(assembly, baseNamespace));
                }
            }
        });

        configurator.ConfigureApiModel(api =>
        {
            api.References.AddRange(_assemblyDescriptors.Select(a => a.assembly));
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(DomainAssembliesBusinessFeature),
                    assembly =>
                    {
                        assembly
                            .AddReferenceFrom<DomainAssembliesBusinessFeature>()
                            .AddCodes(new CasterConfigurerTemplate(domain));

                        foreach (var entity in domain.Types.Having<CasterAttribute>())
                        {
                            entity.Apply(t => assembly.AddReferenceFrom(t));
                        }
                    },
                    usings: [.. CasterConfigurerTemplate.GlobalUsings]
                );
            });
        });

        configurator.ConfigureDomainServiceCollection(services =>
        {
            services.References.AddRange(_assemblyDescriptors.Select(ad => ad.assembly));
            services.Usings.AddRange([
                "Baked.Business",
                "Baked.Runtime",
                "Microsoft.Extensions.DependencyInjection"
            ]);
        });

        configurator.ConfigureServiceProvider(sp =>
        {
            Caster.SetServiceProvider(sp);

            configurator.UsingGeneratedContext(generatedContext =>
            {
                var assembly = generatedContext.Assemblies[nameof(DomainAssembliesBusinessFeature)];

                var type = assembly.GetExportedTypes().SingleOrDefault(t => t.Name.Contains("CasterConfigurer")) ?? throw new("`ICasterConfigurer` implementation not found");
                var typeInstance = (ICasterConfigurer?)Activator.CreateInstance(type) ?? throw new($"Cannot create instance of {type}");

                typeInstance.Configure();
            });
        });

        configurator.ConfigureTestConfiguration(test =>
        {
            test.SetUps.Add(spec =>
            {
                Caster.SetServiceProvider(spec.GiveMe.TheServiceProvider());
            });
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            foreach (var (assembly, _) in _assemblyDescriptors)
            {
                var xmlPath = XmlComments.GetPath(assembly);
                if (xmlPath is null) { continue; }

                swaggerGenOptions.IncludeXmlComments(xmlPath);
            }
        });
    }
}