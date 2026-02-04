using Baked.Architecture;
using Baked.Business;
using Baked.RestApi;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.LocatableExtension;

public class LocatableExtensionCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<LocatableExtensionAttribute>();

            builder.Conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsClass &&
                    !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.TryGetFirstProperty<IdAttribute>(out var _) &&
                    members.TryGetMethods("op_Implicit", out var implicits) &&
                    implicits.Count() == 1 &&
                    implicits.Single().Parameters.SingleOrDefault()?.ParameterType.TryGetMetadata(out var parameterTypeMetadata) == true &&
                    parameterTypeMetadata.Has<LocatableAttribute>(),
                attribute: context =>
                {
                    Console.WriteLine(context.Type.Name);
                    var locatableType = context.Type.GetMembers().GetMethod("op_Implicit").Parameters.Single().ParameterType;

                    return locatableType.Apply(t => new LocatableExtensionAttribute(t));
                },
                order: 20
            );
            builder.Conventions.SetPropertyAttribute(
                when: c => c.Type.Has<LocatableExtensionAttribute>(),
                attribute: c =>
                {
                    var locatableExtensionAttribute = c.Type.GetMetadata().Get<LocatableExtensionAttribute>();

                    return c.Domain.Types[locatableExtensionAttribute.LocatableType].GetMembers().Properties.First(p => p.CustomAttributes.Contains<IdAttribute>()).Get<IdAttribute>();
                },
                order: 20
            );
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Has<LocatableExtensionAttribute>(),
                apply: (c, add) =>
                {
                    var locatableType = c.Type.Get<LocatableExtensionAttribute>().LocatableType;
                    var locatableTypeModel = c.Domain.Types[locatableType];
                    if (!locatableTypeModel.TryGetNamespaceAttribute(out var namespaceAttribute)) { return; }

                    add(c.Type, namespaceAttribute);
                },
                order: 20
            );
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Has<LocatableExtensionAttribute>(),
                apply: (c, set) =>
                {
                    set(c.Type, new ApiInputAttribute());

                    var locatableExtensionType = c.Type;
                    if (!locatableExtensionType.TryGetLocatableTypeFromExtension(c.Domain, out var locatableType)) { return; }
                    if (!locatableType.GetMetadata().CustomAttributes.TryGet<LocatableAttribute>(out var locatable)) { return; }

                    locatableExtensionType.Apply(t => set(c.Type, new LocatableAttribute(typeof(ILocator<>).MakeGenericType(t))));
                },
                order: 20
            );
            builder.Conventions.AddTypeAttributeConfiguration<LocatableAttribute>(
                when: c => c.Type.Has<LocatableExtensionAttribute>() && c.Type.Has<LocatableAttribute>(),
                attribute: locatable =>
                {
                    locatable.LocateRenderer = (serviceExpression, idExpression) => $"{serviceExpression}.Locate({idExpression}, throwNotFound: true)";
                    locatable.LocateManyRenderer = (serviceExpression, idsExpression) => $"{serviceExpression}.LocateMany({idsExpression})";
                },
                order: 20
            );

            builder.Conventions.Add(new ExtensionsUnderLocatablesConvention(), order: RestApiLayer.MaxConventionOrder);
            builder.Conventions.Add(new ExtensionsAreServedUnderLocatableRoutesConvention(), order: RestApiLayer.MaxConventionOrder);
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(LocatableExtensionCodingStyleFeature),
                    assembly =>
                    {
                        var codeTemplate = new LocatorTemplate(domain);
                        assembly.AddCodes(codeTemplate);
                        assembly.AddReferences(codeTemplate.References);
                        assembly.AddReferenceFrom<LocatableExtensionCodingStyleFeature>();
                    },
                    usings: [.. LocatorTemplate.GlobalUsings]
                );
            });
        });

        configurator.ConfigureServiceCollection(services =>
        {
            configurator.UsingGeneratedContext(context =>
            {
                services.AddFromAssembly(context.Assemblies[nameof(LocatableExtensionCodingStyleFeature)]);
            });
        });
    }
}