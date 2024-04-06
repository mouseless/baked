using Do.Communication;
using Newtonsoft.Json;

namespace Do.Test.Communication;

public class ExternalSamples(IClient<ExternalSamples> _client)
{
    public async Task<List<PullRequest>> GitHubPulls()
    {
        var request = new Request("repos/mouseless/do/pulls", HttpMethod.Get);

        var response = await _client.Send(request);

        return JsonConvert.DeserializeObject<List<PullRequest>>(response.Content) ?? [];
    }
}