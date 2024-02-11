using Do.HttpClient;

namespace Do.Communication.Http;

public class HttpClientFactory(IHttpClientFactory _httpClientFactory, List<HttpClientDescriptor> descriptors)
{
    IEnumerable<string> ConfiguredClients { get; } = descriptors.Select(d => d.Name);

    public System.Net.Http.HttpClient Create<T>()
    {
        var clientName = typeof(T).Name;
        var clientConfig = ConfiguredClients.Contains(clientName) ? clientName : "Default";

        return _httpClientFactory.CreateClient(clientConfig);
    }
}
