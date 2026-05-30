using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.Playground.Caching;
using Baked.Ui;

namespace Baked.Playground.Override.Domain;

public class CacheSamplesDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddTypeComponentConfiguration<TabbedPage>(
                when: c => c.Type.Is<CacheSamples>(),
                component: tp =>
                {
                    tp.Schema.Tabs[0].Contents[0].Narrow = true;
                    tp.Schema.Tabs[0].Contents[1].Narrow = true;
                },
                order: Order.At.Override
            );
        });
    }
}