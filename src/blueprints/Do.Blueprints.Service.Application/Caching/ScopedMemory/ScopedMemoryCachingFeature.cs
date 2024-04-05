using Do.Architecture;
using Microsoft.Extensions.Caching.Memory;

namespace Do.Caching.ScopedMemory;

public class ScopedMemoryCachingFeature : IFeature<CachingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddScopedWithFactory<IMemoryCache, MemoryCache>();
        });
    }
}