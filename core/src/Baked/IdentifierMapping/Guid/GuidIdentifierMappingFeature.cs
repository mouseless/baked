using Baked.Architecture;
using Baked.Orm;

namespace Baked.IdentifierMapping.Guid;

public class GuidIdentifierMappingFeature : IFeature<IdentifierMappingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(GuidIdentifierMappingFeature),
                    assembly =>
                    {
                        assembly.AddReferenceFrom<GuidIdentifierMappingFeature>();

                        foreach (var entity in domain.Types.Having<EntityAttribute>())
                        {
                            assembly.AddCodes(new GuidIdMapperTemplate(entity));

                            entity.Apply(t => assembly.AddReferenceFrom(t));
                        }
                    },
                    usings:
                    [
                        "Baked.IdentifierMapping",
                        "Baked.IdentifierMapping.Guid",
                        "Baked.Orm",
                        "Baked.Runtime",
                        "FluentNHibernate",
                        "FluentNHibernate.Automapping",
                        "FluentNHibernate.Diagnostics",
                        "FluentNHibernate.Conventions.Helpers",
                        "FluentNHibernate.Mapping",
                        "Microsoft.Extensions.DependencyInjection",
                        "NHibernate.Linq",
                    ]
                );
            });
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            configurator.UsingGeneratedContext(context =>
            {
                var idMapperTypes = context.Assemblies[nameof(GuidIdentifierMappingFeature)].GetExportedTypes().Where(t => t.IsAssignableTo(typeof(IIdMapper)));
                foreach (var idMapperType in idMapperTypes)
                {
                    var idMapper = (IIdMapper?)Activator.CreateInstance(idMapperType) ?? throw new($"Cannot create instance of {idMapperType}");

                    idMapper.Configure(model);
                }
            });
        });
    }
}
