using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;
using Baked.RestApi.Model;
using Baked.Runtime;
using Humanizer;

namespace Baked.CodingStyle.RichTransient;

public class RichTransientCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<RichTransientAttribute>();

            builder.Conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    members.Has<TransientAttribute>() &&
                    members.TryGetFirstProperty<IdAttribute>(out var idProperty) &&
                    members.Methods.Any(m =>
                        m.Has<InitializerAttribute>() &&
                        m.DefaultOverload.IsPublic &&
                        m.DefaultOverload.Parameters.Count == 1 &&
                        m.DefaultOverload.Parameters.All(p =>
                            p.Name == idProperty.Name.Camelize() &&
                            p.ParameterType == idProperty.PropertyType &&
                            (p.ParameterType.IsValueType || p.ParameterType.Is<string>())
                        )
                    ),
                apply: (c, set) =>
                {
                    set(c.Type, new RichTransientAttribute());
                    set(c.Type, new ApiInputAttribute());
                    c.Type.Apply(t =>
                    {
                        var initializer = c.Type.GetMembers().Methods.First(m => m.Has<InitializerAttribute>() && m.DefaultOverload.IsPublic);
                        var isAsync = initializer.DefaultOverload.ReturnType.IsAssignableTo<Task>();
                        var attribute = new LocatableAttribute(typeof(ILocator<>).MakeGenericType(isAsync ? typeof(Task<>).MakeGenericType(t) : t))
                        {
                            IsAsync = isAsync,
                        };
                        set(c.Type, attribute);
                    });
                },
                order: 10
            );
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Has<RichTransientAttribute>(),
                apply: (c, set) =>
                {
                    c.Type.Apply(t =>
                    {
                        var initializer = c.Type.GetMembers().Methods.First(m => m.Has<InitializerAttribute>() && m.DefaultOverload.IsPublic);
                        set(c.Type, new LocatableAttribute(typeof(ILocator<>).MakeGenericType(t))
                        {
                            IsAsync = initializer.DefaultOverload.ReturnType.IsAssignableTo<Task>()
                        });
                    });
                },
                order: 10
            );
            builder.Conventions.AddTypeAttributeConfiguration<LocatableAttribute>(
                when: c => c.Type.Has<RichTransientAttribute>() && c.Type.Has<LocatableAttribute>(),
                attribute: locatable =>
                {
                    locatable.LocateRenderer = (serviceExpression, idExpression) => locatable.IsAsync
                        ? $"await {serviceExpression}.LocateAsync({idExpression}, throwNotFound = true)"
                        : $"{serviceExpression}.Locate({idExpression}, throwNotFound = true)";
                    locatable.LocateManyRenderer = (serviceExpression, idsExpression) => locatable.IsAsync
                        ? $"await {serviceExpression}.LocateManyAsync({idsExpression})"
                        : $"{serviceExpression}.LocateMany({idsExpression})";
                },
                order: 10
            );
            builder.Conventions.SetMethodAttribute(
                when: c =>
                    c.Type.Has<RichTransientAttribute>() &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.Any(p => p.IsPublic) &&
                    c.Method.Has<InitializerAttribute>() &&
                    c.Method.DefaultOverload.IsPublic,
                attribute: c => new ActionModelAttribute(),
                order: 20
            );

            builder.Conventions.Add(new RichTransientUnderPluralGroupConvention());
            builder.Conventions.Add(new RichTransientInitializerIsGetResourceConvention(), order: 10);
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(RichTransientCodingStyleFeature),
                    assembly =>
                    {
                        var codeTemplate = new LocatorTemplate(domain);
                        assembly.AddCodes(codeTemplate);
                        assembly.AddReferences(codeTemplate.References);
                        assembly.AddReferenceFrom<RichTransientCodingStyleFeature>();
                    },
                    usings: [.. LocatorTemplate.GlobalUsings]
                );
            });
        });

        configurator.ConfigureServiceCollection(services =>
        {
            configurator.UsingGeneratedContext(context =>
            {
                var locatorAdderType = context.Assemblies[nameof(RichTransientCodingStyleFeature)].GetExportedTypes().First(t => t.IsAssignableTo(typeof(IServiceAdder)));
                if (locatorAdderType is not null)
                {
                    var locatorAdder = (IServiceAdder?)Activator.CreateInstance(locatorAdderType) ?? throw new($"Cannot create instance of {locatorAdderType}");

                    locatorAdder.AddServices(services);
                }
            });
        });
    }
}