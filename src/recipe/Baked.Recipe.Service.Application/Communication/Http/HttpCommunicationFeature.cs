using Baked.Architecture;
using Baked.HttpClient;
using Baked.Runtime.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Communication.Http;

public class HttpCommunicationFeature : IFeature<CommunicationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainTypeCollection(types => types.Add(typeof(IClient<>)));

        configurator.ConfigureHttpClients(descriptors =>
        {
            var configurations = Settings.Optional<Dictionary<string, ClientConfig>>("Communication:Http", []).GetSection() ?? [];
            configurations.TryGetValue(HttpClientLayer.DefaultConfigKey, out var defaultSettings);

            foreach (var (key, (baseAddress, defaultHeaders)) in configurations)
            {
                descriptors.Add(
                    new(
                        Name: key,
                        BaseAddress: baseAddress ?? defaultSettings?.BaseAddress,
                        DefaultHeaders: defaultHeaders.Merge(defaultSettings?.DefaultHeaders)
                    )
                );
            }
        });

        configurator.ConfigureServiceCollection(serviceCollection =>
        {
            serviceCollection.AddSingleton(typeof(HttpClientFactory<>));
            serviceCollection.AddSingleton(typeof(IClient<>), typeof(Client<>));
        });
    }
}