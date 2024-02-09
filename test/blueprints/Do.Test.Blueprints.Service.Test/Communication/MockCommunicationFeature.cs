using Do.Communication;

namespace Do.Test.Communication;

public class MockCommunicationFeature : TestServiceSpec
{
    [Test]
    public async Task Returns_default_response_for_not_configured_mocks()
    {
        var client = GiveMe.The<IClient<MockCommunicationFeature>>();

        var response = await client.Send(new Request(UrlOrPath: string.Empty, Method: HttpMethod.Post));

        response.ShouldNotBeNull();
        response.Content.ShouldBe(new { value = "default response" }.ToJsonString());
    }

    [Test]
    public async Task Default_mock_behaviour_can_be_setup_for_given_client_type()
    {
        var client = GiveMe.The<IClient<Singleton>>();

        var response = await client.Send(new Request(UrlOrPath: string.Empty, Method: HttpMethod.Post));

        response.ShouldNotBeNull();
        ((string?)response.GetContentAsObject())?.ShouldBe("test result");
    }

    [Test]
    public async Task Default_mock_behaviour_can_have_multiple_setups()
    {
        var client = GiveMe.The<IClient<Operation>>();

        var response1 = await client.Send(new Request(UrlOrPath: "path1", Method: HttpMethod.Post));

        response1.ShouldNotBeNull();
        ((string?)response1.GetContentAsObject())?.ShouldBe("path1 response");

        var response2 = await client.Send(new Request(UrlOrPath: "path2", Method: HttpMethod.Post));
        response2.ShouldNotBeNull();
        ((string?)response2.GetContentAsObject())?.ShouldBe("path2 response");
    }

    [Test]
    public async Task Default_mock_behaviour_of_a_client_can_be_overriden_during_test_setup()
    {
        var client = MockMe.TheClient<Singleton>(response: "overridden response");

        var response = await client.Send(new Request(UrlOrPath: string.Empty, Method: HttpMethod.Post));

        response.ShouldNotBeNull();
        ((string?)response.GetContentAsObject())?.ShouldBe("overridden response");
    }
}
