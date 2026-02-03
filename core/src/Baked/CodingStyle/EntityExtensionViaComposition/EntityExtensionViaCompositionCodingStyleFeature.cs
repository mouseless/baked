using Baked.Architecture;
using Baked.Business;
using Baked.Orm;
using Baked.RestApi;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class EntityExtensionViaCompositionCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<EntityExtensionAttribute>();

            builder.Conventions.SetTypeAttribute(
                attribute: context =>
                {
                    var entityType = context.Type.GetMembers().GetMethod("op_Implicit").Parameters.Single().ParameterType;

                    return entityType.Apply(t => new EntityExtensionAttribute(t));
                },
                when: c =>
                    c.Type.IsClass &&
                    !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.TryGetFirstProperty<IdAttribute>(out var _) &&
                    members.TryGetMethods("op_Implicit", out var implicits) &&
                    implicits.Count() == 1 &&
                    implicits.Single().Parameters.SingleOrDefault()?.ParameterType.TryGetMetadata(out var parameterTypeMetadata) == true &&
                    parameterTypeMetadata.Has<EntityAttribute>(),
                order: 10
            );
            builder.Conventions.SetPropertyAttribute(
                when: c => c.Type.Has<EntityExtensionAttribute>(),
                attribute: c =>
                {
                    var entityExtensionsAttribute = c.Type.GetMetadata().Get<EntityExtensionAttribute>();

                    return c.Domain.Types[entityExtensionsAttribute.EntityType].GetMembers().Properties.First(p => p.CustomAttributes.Contains<IdAttribute>()).Get<IdAttribute>();
                },
                order: 10
            );
            builder.Conventions.SetTypeAttribute(
                apply: (c, add) =>
                {
                    var entityType = c.Type.Get<EntityExtensionAttribute>().EntityType;
                    var entityTypeModel = c.Domain.Types[entityType];
                    if (!entityTypeModel.TryGetNamespaceAttribute(out var namespaceAttribute)) { return; }

                    add(c.Type, namespaceAttribute);
                },
                when: c => c.Type.Has<EntityExtensionAttribute>(),
                order: 10
            );
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Has<EntityExtensionAttribute>(),
                apply: (c, set) =>
                {
                    set(c.Type, new ApiInputAttribute());

                    var entityExtensionType = c.Type;
                    if (!entityExtensionType.TryGetEntityTypeFromExtension(c.Domain, out var entityType)) { return; }
                    if (!entityType.GetMetadata().CustomAttributes.TryGet<LocatableAttribute>(out var entityLocatable)) { return; }

                    entityExtensionType.Apply(t => set(c.Type, new LocatableAttribute(typeof(ILocator<>).MakeGenericType(t))));
                },
                order: 20
            );
            builder.Conventions.AddTypeAttributeConfiguration<LocatableAttribute>(
                when: c => c.Type.Has<EntityExtensionAttribute>() && c.Type.Has<LocatableAttribute>(),
                attribute: locatable =>
                {
                    locatable.LocateRenderer = (serviceExpression, idExpression) => $"{serviceExpression}.Locate({idExpression}, throwNotFound: true)";
                    locatable.LocateManyRenderer = (serviceExpression, idsExpression) => $"{serviceExpression}.LocateMany({idsExpression})";
                },
                order: 20
            );

            builder.Conventions.Add(new EntityExtensionsUnderEntitiesConvention(), order: RestApiLayer.MaxConventionOrder);
            builder.Conventions.Add(new ExtensionsAreServedUnderEntityRoutesConvention(), order: RestApiLayer.MaxConventionOrder);
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(EntityExtensionViaCompositionCodingStyleFeature),
                    assembly =>
                    {
                        var codeTemplate = new LocatorTemplate(domain);
                        assembly.AddCodes(codeTemplate);
                        assembly.AddReferences(codeTemplate.References);
                        assembly.AddReferenceFrom<EntityExtensionViaCompositionCodingStyleFeature>();
                    },
                    usings: [.. LocatorTemplate.GlobalUsings]
                );
            });
        });

        configurator.ConfigureServiceCollection(services =>
        {
            configurator.UsingGeneratedContext(context =>
            {
                services.AddFromAssembly(context.Assemblies[nameof(EntityExtensionViaCompositionCodingStyleFeature)]);
            });
        });
    }
}