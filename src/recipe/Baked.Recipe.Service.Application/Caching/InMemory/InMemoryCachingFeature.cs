using Baked.Architecture;
using Baked.Runtime;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Caching.InMemory;

public class InMemoryCachingFeature(Action<MemoryCacheOptions> _options, Setting<TimeSpan> clientExpiration)
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
            app.Plugins.Add(
                new CacheApplicationPlugin { ExpirationInMinutes = (int)clientExpiration.GetValue().TotalMinutes }
            );
        });

        configurator.ConfigureTestConfiguration(test =>
        {
            test.TearDowns.Add(spec =>
            {
                spec.GiveMe.TheMemoryCache(clear: true);
            });
        });
    }
}