using Baked.Architecture;
using Baked.Authorization;
using Baked.Business;
using Baked.Lifetime;

namespace Baked.Playground.Override.Domain;

// Note this is for demo purposes
public class AttributeExportOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureAttributeExportCollection(exports =>
        {
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

            exports.RestApi(restApi =>
            {
                restApi.Include<RequireUserAttribute>(_ => false);
                restApi.Include<AllowAnonymousAttribute>();
            });
        });
    }
}