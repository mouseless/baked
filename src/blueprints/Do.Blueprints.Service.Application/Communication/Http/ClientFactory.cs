using Microsoft.Extensions.Configuration;

namespace Do.Communication.Http;

public class ClientFactory<T>(IConfiguration _configuration, IHttpClientFactory _httpClientFactory)
{
    public System.Net.Http.HttpClient Create()
    {
        var name = typeof(T).Name;
        var client = _httpClientFactory.CreateClient(name);

        client.BaseAddress ??= _configuration.GetValue<Uri>($"Communication:Http:{name}:BaseAddress") ??
            _configuration.GetValue<Uri>($"Communication:Http:Default:BaseAddress");

        var defaultHeaders = _configuration.GetSection($"Communication:Http:Default:DefaultHeaders").Get<Dictionary<string, string>>() ?? [];
        var typeHeaders = _configuration.GetSection($"Communication:Http:{name}:DefaultHeaders").Get<Dictionary<string, string>>() ?? [];

        foreach (var (key, value) in typeHeaders)
        {
            defaultHeaders[key] = value;
        }

        foreach (var (key, value) in defaultHeaders)
        {
            client.DefaultRequestHeaders.Add(key, value);
        }

        return client;
    }
}
