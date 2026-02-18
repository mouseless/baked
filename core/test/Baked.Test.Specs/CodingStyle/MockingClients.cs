using Baked.Playground.Communication;
using Baked.RestApi.Model;

namespace Baked.Test.CodingStyle;

public class MockingClients : TestSpec
{
    [Test]
    public void Client_implementations_are_not_controllers()
    {
        var domain = GiveMe.TheDomainModel();
        var gitHubClient = domain.Types[typeof(GitHubClient)];

        gitHubClient.TryGetMetadata(out var metadata).ShouldBeTrue();
        metadata.Has<ControllerModelAttribute>().ShouldBeFalse();
    }

    [Test]
    public void Singleton_mock_is_registered_for_client_interfaces()
    {
        var client = GiveMe.The<IGitHubClient>();

        client.ShouldNotBeOfType<GitHubClient>();
        Mock.Get(client).ShouldNotBeNull();
    }
}