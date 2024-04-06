namespace Do.Communication.Http;

public class HttpClientFactory<T>(IHttpClientFactory _httpClientFactory)
{
    public System.Net.Http.HttpClient Create() => _httpClientFactory.CreateClient(typeof(T).Name);
}