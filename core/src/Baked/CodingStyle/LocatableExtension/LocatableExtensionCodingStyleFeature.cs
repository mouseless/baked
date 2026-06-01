using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.LocatableExtension;

public class LocatableExtensionCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Type.Add<LocatableExtensionAttribute>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsClass &&
                    !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.Any(p => p.CustomAttributes.Contains<IdAttribute>()) &&
                    members.TryGetMethods("op_Implicit", out var implicits) &&
                    implicits.Count() == 1 &&
                    implicits.Single().Parameters.SingleOrDefault()?.ParameterType.TryGetMetadata(out var parameterTypeMetadata) == true &&
                    parameterTypeMetadata.Has<LocatableAttribute>(),
                attribute: context =>
                {
                    var locatableType = context.Type.GetMembers().GetMethod("op_Implicit").Parameters.Single().ParameterType;

                    return locatableType.Apply(t => new LocatableExtensionAttribute(t));
                },
                order: Order.At.Defaults + 20
            );
            conventions.SetPropertyAttribute(
                when: c => c.Type.Has<LocatableExtensionAttribute>(),
                attribute: c =>
                {
                    var locatableExtensionAttribute = c.Type.GetMetadata().Get<LocatableExtensionAttribute>();

                    return c.Domain.Types[locatableExtensionAttribute.LocatableType].GetMembers().Properties.First(p => p.CustomAttributes.Contains<IdAttribute>()).Get<IdAttribute>();
                },
                order: Order.At.Defaults + 20
            );
            conventions.SetTypeAttribute(
                when: c => c.Type.Has<LocatableExtensionAttribute>(),
                apply: (c, set) =>
                {
                    var locatableType = c.Type.Get<LocatableExtensionAttribute>().LocatableType;
                    var locatableTypeModel = c.Domain.Types[locatableType];
                    if (!locatableTypeModel.TryGetNamespaceAttribute(out var namespaceAttribute)) { return; }

                    set(c.Type, namespaceAttribute);
                },
                order: Order.At.Defaults + 20
            );
            conventions.SetTypeAttribute(
                when: c => c.Type.Has<LocatableExtensionAttribute>(),
                apply: (c, set) =>
                {
                    set(c.Type, new ApiInputAttribute());

                    var locatableExtensionType = c.Type;
                    if (!locatableExtensionType.TryGetLocatableTypeFromExtension(c.Domain, out var locatableType)) { return; }
                    if (!locatableType.GetMetadata().CustomAttributes.TryGet<LocatableAttribute>(out var locatable)) { return; }

                    set(c.Type, new LocatableAttribute());
                },
                order: Order.At.Defaults + 20
            );

            conventions.Add(new ExtensionsUnderLocatablesConvention(), order: Order.At.AbsoluteMax); // TODO consider using Order.At.Max
            conventions.Add(new ExtensionsAreServedUnderLocatableRoutesConvention(), order: Order.At.AbsoluteMax); // TODO consider using Order.At.Max
        });

        configurator.Buildtime.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(LocatableExtensionCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<LocatableExtensionCodingStyleFeature>()
                        .AddCodes(new LocatorTemplate(domain)),
                    usings: [.. LocatorTemplate.GlobalUsings]
                );
            });
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            configurator.Buildtime.UsingGeneratedContext(context =>
            {
                services.AddFromAssembly(context.Assemblies[nameof(LocatableExtensionCodingStyleFeature)]);
            });
        });
    }
}