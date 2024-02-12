using Do.HttpClient;

namespace Do.Communication.Http;

public class HttpClientFactory(IHttpClientFactory _httpClientFactory, List<HttpClientDescriptor> descriptors,
    string defaultClientName = "Default"
)
{
    string DefaultClientName { get; } = defaultClientName;
    IEnumerable<string> ConfiguredClients { get; } = descriptors.Select(d => d.Name);

    public System.Net.Http.HttpClient Create<T>()
    {
        var clientName = typeof(T).Name;
        var clientConfig = ConfiguredClients.Contains(clientName) ? clientName : DefaultClientName;

        return _httpClientFactory.CreateClient(clientConfig);
    }
}
