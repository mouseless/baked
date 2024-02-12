using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Communication.Http;

public class HttpCommunicationFeature : IFeature<CommunicationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTypeCollection(types => types.Add(typeof(IClient<>)));

        configurator.ConfigureHttpClients(descriptors =>
        {
            var configurations = Settings.Optional<Dictionary<string, ClientConfig>>($"Communication:Http", []).GetSection() ?? [];
            configurations.TryGetValue("Default", out var defaultSettings);

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

            configurator.Context.GetServiceCollection().AddSingleton(typeof(HttpClientFactory), sp =>
                new HttpClientFactory(sp.GetRequiredService<IHttpClientFactory>(), descriptors, defaultClientName: "Deafult")
            );
        });

        configurator.ConfigureServiceCollection(serviceCollection =>
        {
            serviceCollection.AddSingleton(typeof(IClient<>), typeof(Client<>));
        });
    }
}

public static class DictionaryExtensions
{
    public static Dictionary<string, string> Merge(this Dictionary<string, string>? to, Dictionary<string, string>? source)
    {
        to ??= [];
        source ??= [];

        foreach (var (key, value) in to)
        {
            source[key] = value;
        }

        return source;
    }
}

