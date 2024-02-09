using Do.HttpClient;
using Microsoft.Extensions.Configuration;

namespace Do.Communication.Http;

public class ClientFactory<T>(IConfiguration _configuration, IHttpClientFactory _httpClientFactory)
{
    public System.Net.Http.HttpClient Create()
    {
        var name = typeof(T).Name;
        var client = _httpClientFactory.CreateClient(name);

        HttpClientDescriptor? descriptor = _configuration.GetSection($"Communication:Http:{name}").Get<HttpClientDescriptor>();

        if (descriptor is not null)
        {
            client.BaseAddress = descriptor.BaseAddress;

            foreach (var header in descriptor.DefaultHeaders ?? [])
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        return client;
    }
}
