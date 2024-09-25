using Baked.Architecture;
using Microsoft.Extensions.Caching.Memory;

namespace Baked.Caching.ScopedMemory;

public class ScopedMemoryCachingFeature : IFeature<CachingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddScopedWithFactory<IMemoryCache, MemoryCache>();
        });

        configurator.ConfigureTestConfiguration(test =>
        {
            test.TearDowns.Add(spec =>
            {
                spec.GiveMe.AMemoryCache(clear: true);
            });
        });
    }
}