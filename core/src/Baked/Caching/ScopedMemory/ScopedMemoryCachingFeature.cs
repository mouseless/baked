using Baked.Architecture;
using Baked.Caching;
using Baked.Caching.ScopedMemory;
using Baked.Domain.Configuration;
using Baked.Testing;
using Baked.Ui;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Baked;

public static class ScopedMemoryCachingFeature
{
    public static IFeature<CachingFeature.Configurator> ScopedMemory(this CachingFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditMethodSchema<RemoteData>(
                schema: rd => rd.SetAttribute("client-cache", "user"),
                when: c => c.Method.TryGet<ClientCache>(out var clientCache) && clientCache.Type == "user",
                order: Order.At.Infra
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
    });

    extension(Stubber giveMe)
    {
        public IMemoryCache AMemoryCache(
            bool clear = false
        )
        {
            var getMemoryCache = giveMe.The<Func<IMemoryCache>>();
            var memoryCache = getMemoryCache();

            if (clear)
            {
                if (memoryCache is not MemoryCache concreteMemoryCache) { throw new AssertionException("Cache cannot be cleared because it is not a `MemoryCache` instance"); }

                concreteMemoryCache.Clear();
            }

            return memoryCache;
        }
    }
}