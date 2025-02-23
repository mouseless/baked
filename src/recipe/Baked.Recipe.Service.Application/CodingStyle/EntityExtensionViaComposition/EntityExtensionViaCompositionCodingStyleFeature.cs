using Baked.Architecture;
using Baked.Business;
using Baked.Orm;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class EntityExtensionViaCompositionCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(
                attribute: context =>
                {
                    var entityType = context.Type.GetMembers().GetMethod("op_Implicit").Parameters.Single().ParameterType;

                    return entityType.Apply(t => new EntityExtensionAttribute(t));
                },
                when: c =>
                    c.Type.IsClass &&
                    !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.TryGetMethods("op_Implicit", out var implicits) &&
                    implicits.Count() == 1 &&
                    implicits.Single().Parameters.SingleOrDefault()?.ParameterType.TryGetMetadata(out var parameterTypeMetadata) == true &&
                    parameterTypeMetadata.Has<EntityAttribute>(),
                order: 10
            );
            builder.Conventions.RemoveTypeMetadata<NamespaceAttribute>(c => c.Type.Has<EntityExtensionAttribute>(), order: 10);
            builder.Conventions.AddTypeMetadata(
                apply: (c, add) =>
                {
                    var entityType = c.Type.GetSingle<EntityExtensionAttribute>().EntityType;
                    var entityTypeModel = c.Domain.Types[entityType];
                    if (!entityTypeModel.TryGetNamespaceAttribute(out var namespaceAttribute)) { return; }

                    add(c.Type, namespaceAttribute);
                },
                when: c => c.Type.Has<EntityExtensionAttribute>(),
                order: 10
            );
            builder.Conventions.AddTypeMetadata(
                apply: (c, add) =>
                {
                    add(c.Type, new ApiInputAttribute());
                    add(c.Type, new LocatableAttribute());
                },
                when: c => c.Type.Has<EntityExtensionAttribute>(),
                order: 10
            );

            builder.Conventions.Add(new EntityExtensionsUnderEntitiesConvention());
            builder.Conventions.Add(new LookupEntityExtensionByIdConvention());
            builder.Conventions.Add(new LookupEntityExtensionsByIdsConvention());
            builder.Conventions.Add(new TargetEntityExtensionFromRouteConvention(), order: 20);
            builder.Conventions.Add(new TargetEntityExtensionFromRouteByUniquePropertiesConvention(), order: 20);
        });
    }
}