using Baked.Architecture;
using Baked.Business;
using Baked.Orm;
using FluentNHibernate.Automapping;

namespace Baked.Playground.Override.Domain;

// Note this is for demo purposes
public class MetadataSetOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureMetadataSetConfigurationCollection(sets =>
        {
            sets.GetOrCreate(nameof(AutoMap))
                .ConfigureBuilderOptions(options =>
                {
                    options.AddAttribute<EntityAttribute>();
                    options.AddAttribute<IdAttribute>();
                    options.AddAttribute<LabelAttribute>();

                    options.ExcludeTypesMissingAttributes = true;
                })
            ;
        });
    }
}