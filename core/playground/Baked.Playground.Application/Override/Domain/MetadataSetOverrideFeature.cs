using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;
using Baked.Orm;

namespace Baked.Playground.Override.Domain;

// Note this is for demo purposes
public class MetadataSetOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureMetadataSetConfigurationCollection(sets =>
        {
            sets.GetOrCreate("Orm")
                .ConfigureBuilderOptions(options =>
                {
                    options.AddAttribute<EntityAttribute>();
                    options.AddAttribute<IdAttribute>();
                    options.AddAttribute<LabelAttribute>();

                    options.ExcludeTypesMissingAttributes = true;
                })
            ;

            sets.GetOrCreate("Lifetime")
                .ConfigureBuilderOptions(options =>
                {
                    options.AddAttribute<SingletonAttribute>();
                    options.AddAttribute<ScopedAttribute>();
                    options.AddAttribute<TransientAttribute>();
                    options.AddAttribute<InitializerAttribute>();

                    options.ExcludeTypesMissingAttributes = true;
                    options.TypeGroupName = type =>
                            type.Has<SingletonAttribute>() ? "Singleton" :
                            type.Has<ScopedAttribute>() ? "Scoped" :
                            type.Has<TransientAttribute>() ? "Transient" :
                            type.Name;
                })
            ;
        });
    }
}