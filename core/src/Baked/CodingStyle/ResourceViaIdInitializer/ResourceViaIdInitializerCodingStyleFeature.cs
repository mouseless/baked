using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.RestApi.Model;
using Humanizer;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.ResourceViaIdInitializer;

public class ResourceViaIdInitializerCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
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
                    set(c.Type, new ApiInputAttribute());
                    set(c.Type, new LocatableAttribute());
                },
                order: Order.At.Infra + 10
            );
            conventions.EditTypeAttribute<LocatableAttribute>(
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
                        .AddReferenceFrom<ResourceViaIdInitializerCodingStyleFeature>()
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
    }

    static bool TryFindIdProperty(TypeModelMembers members, [NotNullWhen(true)] out PropertyModel? property)
    {
        property = members.Properties.FirstOrDefault(p => p.CustomAttributes.Contains<IdProperty>());

        return property is not null;
    }
}