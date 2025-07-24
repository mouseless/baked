using Baked.Architecture;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Caching.InMemory;

public class InMemoryCachingFeature(Action<MemoryCacheOptions> _options)
    : IFeature<CachingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddMemoryCache(_options);
        });

        configurator.ConfigureAppDescriptor(app =>
        {
            app.Plugins.Add(new CacheApplicationPlugin());
        });
    }
}