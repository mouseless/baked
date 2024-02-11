using Do.HttpClient;

namespace Do.Communication.Http;

public class ClientFactory(IHttpClientFactory _httpClientFactory,
    Dictionary<string, HttpClientDescriptor>? _descriptors = default
)
{
    HttpClientDescriptor? DefaultClientConfiguration { get; } = _descriptors?["Default"];

    public System.Net.Http.HttpClient Create<T>()
    {
        var clientName = typeof(T).Name;
        var client = _httpClientFactory.CreateClient(clientName);

        if (_descriptors?.ContainsKey(clientName) == true) { return client; }

        client.BaseAddress = DefaultClientConfiguration?.BaseAddress;

        foreach (var (key, value) in DefaultClientConfiguration?.DefaultHeaders ?? [])
        {
            client.DefaultRequestHeaders.Add(key, value);
        }

        return client;
    }
}
