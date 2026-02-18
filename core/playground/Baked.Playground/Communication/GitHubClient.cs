using Baked.Communication;
using Newtonsoft.Json;

namespace Baked.Playground.Communication;

public class GitHubClient(IClient<GitHubClient> _client)
    : IGitHubClient
{
    public async Task<List<PullRequest>> GetPullRequests(string organization, string repository)
    {
        var request = new Request($"repos/{organization}/{repository}/pulls", HttpMethod.Get);
        var response = await _client.Send(request);

        return JsonConvert.DeserializeObject<List<PullRequest>>(response.Content) ?? [];
    }
}