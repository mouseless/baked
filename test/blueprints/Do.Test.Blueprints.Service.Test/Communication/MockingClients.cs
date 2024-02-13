namespace Do.Test.Communication;

public class MockingClients : TestServiceSpec
{
    [Test]
    public async Task Default_mock_behaviour_can_be_setup_for_given_client_type()
    {
        var client = MockMe.TheClient<Singleton>();

        var response = await client.Send(new(UrlOrPath: string.Empty, Method: HttpMethod.Post));

        response.ShouldNotBeNull();
        response.Content.ShouldBe("\"test result\"");  // this reponse result is configured through Communication.Mock feature in TestServiceSpec
    }

    [Test]
    public async Task Default_mock_behaviour_can_have_multiple_setups()
    {
        var client = MockMe.TheClient<Operation>();

        var response1 = await client.Send(new(UrlOrPath: "path1", Method: HttpMethod.Post));
        response1.ShouldNotBeNull();
        response1.Content.ShouldBe("\"path1 response\"");  // this reponse result is configured through Communication.Mock feature in TestServiceSpec

        var response2 = await client.Send(new(UrlOrPath: "path2", Method: HttpMethod.Post));
        response2.ShouldNotBeNull();
        response2.Content.ShouldBe("\"path2 response\"");  // this reponse result is configured through Communication.Mock feature in TestServiceSpec
    }

    [Test]
    public async Task Default_mock_behaviour_of_a_client_can_be_overriden_during_test_setup()
    {
        var client = MockMe.TheClient<Singleton>(response: "overridden response");

        var response = await client.Send(new(UrlOrPath: string.Empty, Method: HttpMethod.Post));

        response.ShouldNotBeNull();
        response.Content.ShouldBe("\"overridden response\"");
    }
}
