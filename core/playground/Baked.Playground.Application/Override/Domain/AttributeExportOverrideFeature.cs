using Baked.Architecture;
using Baked.Business;
using Baked.Orm;

namespace Baked.Playground.Override.Domain;

public class AttributeExportOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureAttributeExportConfigurations(exports =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                exports.Build("AutoMap",
                    export =>
                    {
                        export.Include<EntityAttribute>();
                        export.Include<IdAttribute>()
                            .ExcludeProperty(p => p.Name == nameof(IdAttribute.RouteName));
                        export.Include<UniqueAttribute>();
                        export.Include<QueryAttribute>()
                            .AddFilter((query, _) => domain.Types[query.LocatableType].GetMetadata().Has<EntityAttribute>())
                            .ExcludeProperty();

                        export.TypeGroupName(type =>
                            type.TryGetLocatableType(domain, out var locatableType) ? locatableType.Name :
                            type.Name
                        );
                    });
            });
        });
    }
}