using Baked.Architecture;
using Baked.Runtime;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Caching.ScopedMemory;

public class ScopedMemoryCachingFeature(Setting<TimeSpan> clientExpiration)
    : IFeature<CachingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<Func<IMemoryCache>>(sp => () => sp.UsingCurrentScope().GetRequiredKeyedService<IMemoryCache>("ScopedMemory"));
            services.AddKeyedScoped<IMemoryCache, MemoryCache>("ScopedMemory");
        });

        configurator.ConfigureAppDescriptor(app =>
        {
            app.Plugins.Add(
                new CacheUserPlugin { ExpirationInMinutes = (int)clientExpiration.GetValue().TotalMinutes }
            );
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