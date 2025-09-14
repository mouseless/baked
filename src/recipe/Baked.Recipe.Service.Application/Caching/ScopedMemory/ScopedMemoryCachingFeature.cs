using Baked.Architecture;
using Baked.Runtime;
using Baked.Ui;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Caching.ScopedMemory;

public class ScopedMemoryCachingFeature(Setting<TimeSpan> clientExpiration)
    : IFeature<CachingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddMethodSchemaConvention<RemoteData>(
                schema: rd => rd.SetAttribute("client-cache", "user"),
                whenMethod: c => c.Method.TryGet<ClientCacheAttribute>(out var clientCache) && clientCache.Type == "user"
            );
        });

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