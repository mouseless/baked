using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;
using Baked.Orm;

namespace Baked.Playground.Override.Domain;

// Note this is for demo purposes
public class AttributeExportOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureAttributeExportCollection(exports =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                exports.Build("Orm",
                    export =>
                    {
                        export.Include<EntityAttribute>();
                        export.Include<IdAttribute>();
                        export.Include<LabelAttribute>();
                        export.Include<QueryAttribute>();

                        export.TypeGroupName(type =>
                                type.TryGetLocatableType(domain, out var locatableType) ? locatableType.Name :
                                type.Name
                            );
                    });
            });

            exports.Build("Business",
                export =>
                {
                    export.Include<SingletonAttribute>();
                    export.Include<ScopedAttribute>();
                    export.Include<TransientAttribute>();
                    export.Include<InitializerAttribute>();
                    export.Include<LocatableAttribute>();

                    export.TypeGroupName(type =>
                            type.Has<SingletonAttribute>() ? "Singleton" :
                            type.Has<ScopedAttribute>() ? "Scoped" :
                            type.Has<TransientAttribute>() ? "Transient" :
                            type.Name
                        );
                }
            );
        });
    }
}