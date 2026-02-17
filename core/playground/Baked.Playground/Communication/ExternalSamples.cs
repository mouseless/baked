namespace Baked.Playground.Communication;

public class ExternalSamples(IGitHubClient _github)
{
    public async Task<List<PullRequest>> GetPullRequests() =>
        await _github.GetPullRequests(organization: "mouseless", repository: "baked");
}