using Baked.Architecture;
using Baked.CodeGeneration;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.RestApi;
using Baked.RestApi.Conventions;
using Baked.RestApi.Model;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
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
            builder.BindingFlags.Property = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

            builder.DefaultOverloadSelector = _defaultOverloadSelector;

            builder.BuildLevels.Add(context => context.DomainTypesContain(context.Type), BuildLevels.Members);
            builder.BuildLevels.Add(context => context.Type.IsGenericType && context.DomainTypesContain(context.Type.GetGenericTypeDefinition()), BuildLevels.Members);
            builder.BuildLevels.Add(BuildLevels.Metadata);

            builder.Index.Type.Add<ServiceAttribute>();
            builder.Index.Type.Add<CasterAttribute>();

            builder.Conventions.AddTypeMetadata(
                apply: (context, add) =>
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

                    add(context.Type, new NamespaceAttribute(@namespace));
                },
                when: c => setNamespaceWhen(c.Type)
            );
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
            api.Usings.Add("Swashbuckle.AspNetCore.Annotations");

            var methodExampleDictionary = new Dictionary<string, RequestResponseExampleData>();
            var domainModel = configurator.Context.GetDomainModel();
            foreach (var type in domainModel.Types.Having<ApiServiceAttribute>())
            {
                if (type.FullName is null) { continue; }

                var controller = new ControllerModel(type) { ClassName = type.CSharpFriendlyFullName.Split('.').Skip(1).Join('_') };
                foreach (var method in type.GetMembers().Methods.Having<ApiMethodAttribute>())
                {
                    controller.AddAction(type, method);

                    var typeExample = new RequestResponseExampleData(
                        type.GetMembers().Documentation.GetExampleCode("request"),
                        type.GetMembers().Documentation.GetExampleCode("response")
                    );

                    var methodExample = new RequestResponseExampleData(
                        method.Documentation.GetExampleCode("request"),
                        method.Documentation.GetExampleCode("response")
                    );

                    methodExampleDictionary.TryAdd($"{type.FullName}", typeExample);
                    methodExampleDictionary.TryAdd($"{type.FullName}.{method.Name}", methodExample);
                }

                if (!controller.Action.Any()) { continue; }

                api.Controller.Add(controller.Id, controller);
            }

            var files = configurator.Context.Get<IGeneratedFileCollection>();
            files.Add("RequestResponseExamples", JsonConvert.SerializeObject(methodExampleDictionary), "json");
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            var domain = configurator.Context.GetDomainModel();

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
                compilationOptions => compilationOptions.WithUsings(
                    "Baked",
                    "Baked.Business",
                    "Baked.Business.DomainAssemblies",
                    "Baked.Runtime",
                    "Microsoft.Extensions.DependencyInjection",
                    "System",
                    "System.Linq",
                    "System.Collections",
                    "System.Collections.Generic",
                    "System.Threading.Tasks"
                )
            );
        });

        configurator.ConfigureServiceProvider(sp =>
        {
            Caster.SetServiceProvider(sp);

            var assembly = configurator.Context.GetGeneratedAssembly(nameof(DomainAssembliesBusinessFeature));

            var type = assembly.GetExportedTypes().SingleOrDefault(t => t.Name.Contains("CasterConfigurer")) ?? throw new("ICasterConfigurer implementation not found");
            var typeInstance = (ICasterConfigurer?)Activator.CreateInstance(type) ?? throw new($"Cannot create instance of {type}");

            typeInstance.Configure();
        });

        configurator.ConfigureTestConfiguration(test =>
        {
            test.SetUps.Add(spec =>
            {
                Caster.SetServiceProvider(spec.GiveMe.TheServiceProvider());
            });
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            // TODO couldn't find a better way to create a shared variable
            // between layer configurators within a feature
            configurator.Context.Add(new TagDescriptions());

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
            conventions.Add(new ConsumesJsonConvention(_when: c => c.Action.HasBody), order: 10);
            conventions.Add(new ProducesJsonConvention(_when: c => !c.Action.Return.IsVoid), order: 10);
            conventions.Add(new UseDocumentationAsDescriptionConvention(configurator.Context.Get<TagDescriptions>()), order: 10);
            conventions.Add(new AddMappedMethodAttributeConvention());
        });

        configurator.ConfigureGeneratedFileCollection(files =>
        {
            if (configurator.Context.Has<TagDescriptions>())
            {
                files.Add(nameof(TagDescriptions), JsonConvert.SerializeObject(configurator.Context.Get<TagDescriptions>()), ".json");
            }
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            foreach (var (assembly, _) in _assemblyDescriptors)
            {
                var xmlPath = XmlComments.GetPath(assembly);
                if (xmlPath is null) { continue; }

                swaggerGenOptions.IncludeXmlComments(xmlPath);
            }

            swaggerGenOptions.EnableAnnotations();
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

            var fileProvider = configurator.Context.GetGeneratedFileProvider();
            using (var file = new FileStream(fileProvider[nameof(TagDescriptions)], FileMode.Open))
            {
                var tagDescriptions = JsonConvert.DeserializeObject<TagDescriptions>(new StreamReader(file).ReadToEnd());
                swaggerGenOptions.DocumentFilter<ApplyTagDescriptionsDocumentFilter>(tagDescriptions);
            }

            using (var file = new FileStream(fileProvider["RequestResponseExamples"], FileMode.Open))
            {
                var methodExamplesDictionary = JsonConvert.DeserializeObject<Dictionary<string, RequestResponseExampleData>>(new StreamReader(file).ReadToEnd()) ?? [];
                swaggerGenOptions.OperationFilter<XmlExamplesOperationFilter>(methodExamplesDictionary);
            }
        });
    }
}