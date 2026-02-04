using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.RestApi.Model;
using Humanizer;
using System.Diagnostics.CodeAnalysis;

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
                    TryFindIdProperty(members, out var idProperty) &&
                    members.Methods.Any(m =>
                        m.Has<InitializerAttribute>() &&
                        m.DefaultOverload.IsPublic &&
                        m.DefaultOverload.Parameters.Count == 1 &&
                        m.DefaultOverload.Parameters.All(p =>
                            p.Name == idProperty.Name.Camelize() &&
                            p.ParameterType == idProperty.PropertyType
                        )
                    ),
                apply: (c, set) =>
                {
                    set(c.Type, new RichTransientAttribute());
                    set(c.Type, new ApiInputAttribute());
                },
                order: 10
            );
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Has<RichTransientAttribute>(),
                apply: (c, set) =>
                {
                    set(c.Type, new LocatableAttribute());
                },
                order: 10
            );
            builder.Conventions.AddTypeAttributeConfiguration<LocatableAttribute>(
                when: c => c.Type.Has<RichTransientAttribute>() && c.Type.Has<LocatableAttribute>(),
                attribute: (locatable, c) =>
                {
                    if (!c.Type.TryGetMembers(out var members)) { return; }

                    var initializer = members.Methods.FirstOrDefault(m => m.Has<InitializerAttribute>() && m.DefaultOverload.IsPublic) ?? throw new($"`{c.Type.Name}` should have had public initializer");
                    locatable.IsAsync = initializer.DefaultOverload.ReturnType.IsAssignableTo<Task>();

                    locatable.LocateRenderer = (serviceExpression, idExpression, throwNotFoundExpression) => locatable.IsAsync
                        ? $"await {serviceExpression}.LocateAsync({idExpression}, {throwNotFoundExpression})"
                        : $"{serviceExpression}.Locate({idExpression}, {throwNotFoundExpression})";

                    locatable.LocateManyRenderer = (serviceExpression, idsExpression) => locatable.IsAsync
                        ? $"await {serviceExpression}.LocateManyAsync({idsExpression})"
                        : $"{serviceExpression}.LocateMany({idsExpression})";
                },
                order: 10
            );
            builder.Conventions.SetMethodAttribute(
                when: c =>
                    c.Type.Has<RichTransientAttribute>() &&
                    c.Type.Has<LocatableAttribute>() &&
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
                services.AddFromAssembly(context.Assemblies[nameof(RichTransientCodingStyleFeature)]);
            });
        });
    }

    static bool TryFindIdProperty(TypeModelMembers members, [NotNullWhen(true)] out PropertyModel? property)
    {
        property = members.Properties.FirstOrDefault(p => p.CustomAttributes.Contains<IdAttribute>());

        return property is not null;
    }
}