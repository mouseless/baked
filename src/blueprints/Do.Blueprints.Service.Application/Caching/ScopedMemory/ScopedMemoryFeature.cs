using Do.Architecture;
using Microsoft.Extensions.Caching.Memory;

namespace Do.Caching.ScopedMemory;

public class ScopedMemoryFeature : IFeature<CachingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddScopedWithFactory<IMemoryCache, MemoryCache>();
        });
    }
}
