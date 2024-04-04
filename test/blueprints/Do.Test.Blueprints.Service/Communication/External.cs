using Do.Communication;
using Newtonsoft.Json;

namespace Do.Test;

public class External(IClient<External> _client)
{
    public async Task<List<PullRequest>> TestClient()
    {
        var request = new Request("repos/mouseless/do/pulls", HttpMethod.Get);

        var response = await _client.Send(request);

        return JsonConvert.DeserializeObject<List<PullRequest>>(response.Content) ?? [];
    }
}
