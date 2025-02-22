using Baked.Architecture;
using Baked.Business;
using Baked.Orm;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.EntitySubclassViaComposition;

public class EntitySubclassViaCompositionCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(
                apply: (c, add) =>
                {
                    var entityType = c.Type.GetMembers().GetMethod("op_Explicit").Parameters.Single().ParameterType;
                    entityType.Apply(t =>
                    {
                        add(c.Type, new EntitySubclassAttribute(t, c.Type.Name.Replace(t.Name, string.Empty)));
                    });
                },
                when: c =>
                    c.Type.IsClass &&
                    !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.TryGetMethods("op_Explicit", out var explicits) &&
                    explicits.Count() == 1 &&
                    explicits.Single().Parameters.SingleOrDefault()?.ParameterType.TryGetMetadata(out var parameterTypeMetadata) == true &&
                    parameterTypeMetadata.Has<EntityAttribute>(),
                order: 10
            );
            builder.Conventions.AddTypeMetadata(
                apply: (c, add) =>
                {
                    add(c.Type, new ApiInputAttribute());
                    add(c.Type, new LocatableAttribute());
                },
                when: c => c.Type.Has<EntitySubclassAttribute>(),
                order: 10
            );
            builder.Conventions.AddMethodMetadata(
                attribute: c => new ActionModelAttribute("Post", [c.Type.Name, c.Method.Name], "target"),
                when: c =>
                    c.Type.Has<EntitySubclassAttribute>() && c.Method.Has<InitializerAttribute>() &&
                    c.Method.Overloads.Any(o => o.IsPublic && !o.IsStatic && !o.IsSpecialName && o.AllParametersAreApiInput()),
                order: 30
            );

            builder.Conventions.Add(new EntitySubclassUnderEntitiesConvention());
            builder.Conventions.Add(new EntitySubclassInitializerIsPostResourceConvention());
            builder.Conventions.Add(new TargetEntitySubclassFromRouteConvention(), order: 20);
        });
    }
}