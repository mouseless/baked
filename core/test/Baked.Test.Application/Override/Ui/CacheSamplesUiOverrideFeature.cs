using Baked.Architecture;
using Baked.Test.Caching;
using Baked.Ui;

namespace Baked.Test.Override.Ui;

public class CacheSamplesUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeComponentConfiguration<TabbedPage>(
                component: tp =>
                {
                    tp.Schema.Tabs[0].Contents[0].Narrow = true;
                    tp.Schema.Tabs[0].Contents[1].Narrow = true;
                },
                when: c => c.Type.Is<CacheSamples>()
            );
        });
    }
}