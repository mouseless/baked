namespace Baked.Playground.Communication;

public interface IGitHubClient
{
    Task<List<PullRequest>> GetPullRequests(string organization, string repository);
}