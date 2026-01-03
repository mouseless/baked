using Baked.Architecture;
using Baked.Playground.Caching;
using Baked.Ui;

namespace Baked.Playground.Override.Ui;

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