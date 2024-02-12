using Do.Communication;

namespace Do.Test;

public class Remote(IClient<Remote> _client)
{
    public async Task<dynamic> Process(string path, string method, object? content = default)
    {
        var httpMethod =
            method.Equals(nameof(HttpMethod.Post), StringComparison.CurrentCultureIgnoreCase) ? HttpMethod.Post :
            method.Equals(nameof(HttpMethod.Put), StringComparison.CurrentCultureIgnoreCase) ? HttpMethod.Put :
            method.Equals(nameof(HttpMethod.Delete), StringComparison.CurrentCultureIgnoreCase) ? HttpMethod.Delete :
            HttpMethod.Get;

        var request = new Request(path, httpMethod, new(content));

        var response = await _client.Send(request);

        return response.GetContentAsObject() ?? new { };
    }
}
