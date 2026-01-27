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
                apply: (c, set) =>
                {
                    set(c.Type, new ApiInputAttribute());

                    var entityExtensionType = c.Type;
                    if (!entityExtensionType.TryGetEntityTypeFromExtension(c.Domain, out var entityType)) { return; }
                    if (!entityType.GetMetadata().CustomAttributes.TryGet<LocatableAttribute>(out var entityLocatable)) { return; }

                    set(c.Type, new LocatableAttribute(entityLocatable.ServiceType, entityLocatable.LocateSingleMethodName)
                    {
                        LocateMultipleMethodName = entityLocatable.LocateMultipleMethodName,
                        IsAsync = entityLocatable.IsAsync,
                        IsFactory = entityLocatable.IsFactory,
                        CastTo = c.Type
                    });
                },
                when: c => c.Type.Has<EntityExtensionAttribute>(),
                order: 10
            );

            builder.Conventions.Add(new EntityExtensionsUnderEntitiesConvention());
            builder.Conventions.Add(new ExtensionsAreServedUnderEntityRoutesConvention(), order: RestApiLayer.MaxConventionOrder);
        });
    }
}