using Microsoft.Extensions.Logging;
using System.Text;

namespace Do.Communication.Http;

public class Client<T>(
    ILogger<Client<T>> _logger,
    HttpClientFactory<T> _clientFactory
) : IClient<T>
{
    public async Task<Response> Send(Request request)
    {
        var req = new HttpRequestMessage(request.Method, request.UrlOrPath);

        if (request.Content is not null && !string.IsNullOrWhiteSpace(request.Content.Body))
        {
            req.Content = new StringContent(request.Content.Body, Encoding.UTF8, request.Content.Type);
        }

        foreach (var (name, value) in request.Headers)
        {
            if (string.IsNullOrWhiteSpace(value)) { continue; }

            req.Headers.Add(name, value);
        }

        var client = _clientFactory.Create();
        var res = await client.SendAsync(req);
        var content = await res.Content.ReadAsStringAsync();

        try
        {
            res.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, ex.Message);

            throw new HttpRequestException(ex.Message, new(content), ex.StatusCode);
        }

        return new(content);
    }
}
