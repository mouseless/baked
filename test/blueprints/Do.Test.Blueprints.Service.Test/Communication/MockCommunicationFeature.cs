using Do.Communication;

namespace Do.Test.Communication;

public class MockCommunicationFeature : TestServiceSpec
{
    [Test]
    public async Task Default_mock_behaviour_can_be_setup_when_adding_feature()
    {
        var client = GiveMe.The<IClient<Singleton>>();

        var response = await client.Send(new Request(UrlOrPath: string.Empty, Method: HttpMethod.Post));

        response.ShouldNotBeNull();
        ((string?)response.GetContentAsObject())?.ShouldBe("test result");
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
