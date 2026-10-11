using Baked.Architecture;
using Baked.Binding;
using Baked.Business;
using Baked.Domain.Configuration;

namespace Baked.CodingStyle.ExtensionViaLocatableInitializer;

public class ExtensionViaLocatableInitializerCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Type.Add<LocatableExtension>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsClass &&
                    !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.Any(p => p.CustomAttributes.Contains<IdProperty>()) &&
                    members.Methods.Any(m =>
                        m.Has<Initializer>() &&
                        m.DefaultOverload.Parameters.Count == 1 &&
                        m.DefaultOverload.Parameters.Single().ParameterType.TryGetMetadata(out var parameterTypeMetadata) &&
                        parameterTypeMetadata.Has<Locatable>()
                    ),
                attribute: context =>
                {
                    var locatableType = context.Type.GetMembers().Methods.First(m => m.Has<Initializer>()).DefaultOverload.Parameters.Single().ParameterType;

                    return locatableType.Apply(t => new LocatableExtension(t));
                },
                order: Order.At.Infra + 20
            );
            conventions.SetPropertyAttribute(
                when: c => c.Type.Has<LocatableExtension>(),
                attribute: c =>
                {
                    var locatableExtensionAttribute = c.Type.GetMetadata().Get<LocatableExtension>();

                    return c.Domain.Types[locatableExtensionAttribute.LocatableType].GetMembers().Properties.First(p => p.CustomAttributes.Contains<IdProperty>()).Get<IdProperty>();
                },
                order: Order.At.Infra + 20
            );
            conventions.SetTypeAttribute(
                when: c => c.Type.Has<LocatableExtension>(),
                apply: (c, set) =>
                {
                    var locatableType = c.Type.Get<LocatableExtension>().LocatableType;
                    var locatableTypeModel = c.Domain.Types[locatableType];
                    if (!locatableTypeModel.TryGetNamespace(out var @namespace)) { return; }

                    set(c.Type, @namespace);
                },
                order: Order.At.Infra + 20
            );
            conventions.SetTypeAttribute(
                when: c => c.Type.Has<LocatableExtension>(),
                apply: (c, set) =>
                {
                    set(c.Type, new Bindable());

                    var locatableExtensionType = c.Type;
                    if (!locatableExtensionType.TryGetLocatableTypeFromExtension(c.Domain, out var locatableType)) { return; }
                    if (!locatableType.GetMetadata().CustomAttributes.TryGet<Locatable>(out var locatable)) { return; }

                    set(c.Type, new Locatable());
                },
                order: Order.At.Infra + 20
            );

            conventions.Add(new ExtensionsUnderLocatablesConvention(), order: Order.At.Max);
            conventions.Add(new ExtensionsAreServedUnderLocatableRoutesConvention(), order: Order.At.Max);
        });

        configurator.Buildtime.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(ExtensionViaLocatableInitializerCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<ExtensionViaLocatableInitializerCodingStyleFeature>()
                        .AddCodes(new LocatorTemplate(domain)),
                    usings: [.. LocatorTemplate.GlobalUsings]
                );
            });
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            configurator.Buildtime.UsingGeneratedContext(context =>
            {
                services.AddFromAssembly(context.Assemblies[nameof(ExtensionViaLocatableInitializerCodingStyleFeature)]);
            });
        });
    }
}