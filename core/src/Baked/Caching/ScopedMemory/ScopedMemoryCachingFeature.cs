using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.Ui;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Caching.ScopedMemory;

public class ScopedMemoryCachingFeature : IFeature<CachingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddMethodSchemaConfiguration<RemoteData>(
                schema: rd => rd.SetAttribute("client-cache", "user"),
                when: c => c.Method.TryGet<ClientCacheAttribute>(out var clientCache) && clientCache.Type == "user",
                order: Order.At.Defaults
            );
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<Func<IMemoryCache>>(sp => () => sp.UsingCurrentScope().GetRequiredKeyedService<IMemoryCache>("ScopedMemory"));
            services.AddKeyedScoped<IMemoryCache, MemoryCache>("ScopedMemory");
        });

        configurator.Ui.ConfigureAppDescriptor(app =>
        {
            app.Plugins.Add(new CacheUserPlugin());
        });

        configurator.Testing.ConfigureTestConfiguration(test =>
        {
            test.TearDowns.Add(spec =>
            {
                spec.GiveMe.AMemoryCache(clear: true);
            });
        });
    }
}