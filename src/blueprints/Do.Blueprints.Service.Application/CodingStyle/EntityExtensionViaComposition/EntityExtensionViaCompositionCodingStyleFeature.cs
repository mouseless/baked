using Do.Architecture;
using Do.Business;
using Do.Orm;

namespace Do.CodingStyle.EntityExtensionViaComposition;

public class EntityExtensionViaCompositionCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(
                apply: (c, add) =>
                {
                    var entityType = c.Type.GetMembers().GetMethod("op_Implicit").Parameters.Single().ParameterType;
                    entityType.Apply(t =>
                    {
                        add(c.Type, new EntityExtensionAttribute(t));
                    });
                },
                when: c =>
                    !c.Type.IsAbstract &&
                    !c.Type.IsInterface &&
                    c.Type.TryGetMembers(out var members) &&
                    members.TryGetMethods("op_Implicit", out var implicits) &&
                    implicits.Count() == 1 &&
                    implicits.Single().Parameters.SingleOrDefault()?.ParameterType.TryGetMetadata(out var parameterTypeMetadata) == true &&
                    parameterTypeMetadata.Has<EntityAttribute>(),
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
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            var domain = configurator.Context.GetDomainModel();

            conventions.Add(new TargetEntityExtensionFromRouteConvention(domain));
            conventions.Add(new TargetEntityExtensionFromRouteByUniquePropertiesConvention(domain));
            conventions.Add(new EntityExtensionsUnderEntitiesConvention(domain));
            conventions.Add(new LookupEntityExtensionByIdConvention(domain));
            conventions.Add(new LookupEntityExtensionsByIdsConvention(domain));
        });
    }
}