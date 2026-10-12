using Baked.Architecture;
using Baked.Caching;
using Baked.Caching.InMemory;
using Baked.Domain.Configuration;
using Baked.Testing;
using Baked.Ui;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Baked;

public static class InMemoryCachingFeature
{
    public static IFeature<CachingFeature.Configurator> InMemory(this CachingFeature.Configurator x,
        Action<MemoryCacheOptions>? options = default
    ) => x.Define(configurator =>
    {
        options ??= _ => { };

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditMethodSchema<RemoteData>(
                schema: rd => rd.SetAttribute("client-cache", "application"),
                when: c => c.Method.TryGet<ClientCache>(out var clientCache) && clientCache.Type == "application",
                order: Order.At.Infra
            );
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddMemoryCache(options);
        });

        configurator.Ui.ConfigureAppDescriptor(app =>
        {
            app.Plugins.Add(new CacheApplicationPlugin());
        });

        configurator.Testing.ConfigureTestConfiguration(test =>
        {
            test.TearDowns.Add(spec =>
            {
                spec.GiveMe.TheMemoryCache(clear: true);
            });
        });
    });

    extension(Stubber giveMe)
    {
        public IMemoryCache TheMemoryCache(
            bool clear = false
        )
        {
            var memoryCache = giveMe.The<IMemoryCache>();

            if (clear)
            {
                if (memoryCache is not MemoryCache concreteMemoryCache) { throw new AssertionException("Cache cannot be cleared because it is not a `MemoryCache` instance"); }

                concreteMemoryCache.Clear();
            }

            return memoryCache;
        }
    }
}