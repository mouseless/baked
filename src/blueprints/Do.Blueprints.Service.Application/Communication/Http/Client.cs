using Microsoft.Extensions.Logging;
using System.Text;

namespace Do.Communication.Http;

public class Client<T>(
    ILogger<Client<T>> _logger,
    ClientFactory<T> _clientFactory
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

        var res = await _clientFactory.Create().SendAsync(req);
        var content = await res.Content.ReadAsStringAsync();

        try
        {
            res.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode.HasValue && (int)ex.StatusCode >= 500)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }

            if (string.IsNullOrEmpty(content))
            {
                throw;
            }

            throw new HttpRequestException(content, ex, ex.StatusCode);
        }

        return new(content);
    }
}
