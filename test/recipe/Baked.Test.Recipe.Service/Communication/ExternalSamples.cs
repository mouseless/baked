using Baked.Communication;
using Newtonsoft.Json;

namespace Baked.Test.Communication;

public class ExternalSamples(IClient<ExternalSamples> _client)
{
    public async Task<List<PullRequest>> GetPullRequests()
    {
        var request = new Request("repos/mouseless/baked/pulls", HttpMethod.Get);
        var response = await _client.Send(request);

        return JsonConvert.DeserializeObject<List<PullRequest>>(response.Content) ?? [];
    }
}