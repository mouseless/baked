using Do.Architecture;
using Do.HttpClient;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Communication.Http;

public class HttpCommunicationFeature : IFeature<CommunicationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTypeCollection(types => types.Add(typeof(IClient<>)));

        configurator.ConfigureHttpClients(descriptors =>
        {
            var configurations = Settings.Required<Dictionary<string, ClientConfig>>($"Communication:Http").GetSection() ?? [];
            configurations.Remove("Default", out var defaultSettings);

            foreach (var (key, (baseAddress, defaultHeaders)) in configurations)
            {
                descriptors.Add(
                    key,
                    new(
                        Name: key,
                        BaseAddress: baseAddress ?? defaultSettings?.BaseAddress,
                        DefaultHeaders: defaultHeaders.OverrideDictionary(defaultSettings?.DefaultHeaders)
                    )
                );
            }

            descriptors.Add(
                "Default",
                new(
                    "Deafult", defaultSettings?.BaseAddress, defaultSettings?.DefaultHeaders
                    )
            );

            configurator.Context.GetServiceCollection().AddSingleton(typeof(HttpClientFactory), sp =>
                new HttpClientFactory(sp.GetRequiredService<IHttpClientFactory>(), descriptors)
            );
        });

        configurator.ConfigureServiceCollection(serviceCollection =>
        {
            serviceCollection.AddSingleton(typeof(IClient<>), typeof(Client<>));
        });
    }
}

