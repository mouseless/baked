using Baked.Architecture;
using Baked.Business;
using Baked.Orm;
using Baked.RestApi;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.EntitySubclassViaComposition;

public class EntitySubclassViaCompositionCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<EntitySubclassAttribute>();

            builder.Conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsClass &&
                    !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.TryGetMethods("op_Explicit", out var explicits) &&
                    explicits.Count() == 1 &&
                    explicits.Single().Parameters.SingleOrDefault()?.ParameterType.TryGetMetadata(out var parameterTypeMetadata) == true &&
                    parameterTypeMetadata.Has<EntityAttribute>(),
                attribute: c =>
                {
                    var entityType = c.Type.GetMembers().GetMethod("op_Explicit").Parameters.Single().ParameterType;

                    return entityType.Apply(t => new EntitySubclassAttribute(t, c.Type.Name.Replace(t.Name, string.Empty)));
                },
                order: 10
            );
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Has<EntitySubclassAttribute>(),
                apply: (c, set) =>
                {
                    set(c.Type, new ApiInputAttribute());
                    var entitySubclassType = c.Type;
                    if (!entitySubclassType.TryGetSubclassName(out var subclassName)) { return; }
                    if (!entitySubclassType.TryGetEntityTypeFromSubclass(c.Domain, out var entityType)) { return; }
                    if (!entityType.TryGetQueryType(c.Domain, out var queryType)) { return; }
                    if (!queryType.TryGetMembers(out var queryMembers)) { return; }

                    // TODO requires review
                    var singleByUniqueMethod = queryMembers.Methods.FirstOrDefault(m => m.Name.StartsWith("SingleBy"));
                    if (singleByUniqueMethod is null) { return; }

                    var uniqueParameter = singleByUniqueMethod.DefaultOverload.Parameters.First();
                    if (!uniqueParameter.ParameterType.IsEnum && !uniqueParameter.ParameterType.Is<string>()) { return; }

                    set(c.Type, new LocatableAttribute());
                },
                order: 10
            );
            builder.Conventions.SetMethodAttribute(
                attribute: c => new ActionModelAttribute(),
                when: c =>
                    c.Type.Has<EntitySubclassAttribute>() && c.Method.Has<InitializerAttribute>() &&
                    c.Method.Overloads.Any(o => o.IsPublic && !o.IsStatic && !o.IsSpecialName && o.AllParametersAreApiInput()),
                order: 30
            );

            builder.Conventions.Add(new UniqueIdParameterConvention(), order: RestApiLayer.MaxConventionOrder - 50);
            builder.Conventions.Add(new SubclassesAreServedUnderEntityRoutesConvention(), order: RestApiLayer.MaxConventionOrder);
            builder.Conventions.Add(new EntitySubclassUnderEntitiesConvention(), order: RestApiLayer.MaxConventionOrder);
            builder.Conventions.Add(new EntitySubclassInitializerIsPostResourceConvention(), order: RestApiLayer.MaxConventionOrder);
            builder.Conventions.Add(new AddSubclassNameToRouteConvention(), order: RestApiLayer.MaxConventionOrder);
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(EntitySubclassViaCompositionCodingStyleFeature),
                    assembly =>
                    {
                        var codeTemplate = new LocatorTemplate(domain);
                        assembly.AddCodes(codeTemplate);
                        assembly.AddReferences(codeTemplate.References);
                        assembly.AddReferenceFrom<EntitySubclassViaCompositionCodingStyleFeature>();
                    },
                    usings: [.. LocatorTemplate.GlobalUsings]
                );
            });
        });

        configurator.ConfigureServiceCollection(services =>
        {
            configurator.UsingGeneratedContext(context =>
            {
                services.AddFromAssembly(context.Assemblies[nameof(EntitySubclassViaCompositionCodingStyleFeature)]);
            });
        });
    }
}