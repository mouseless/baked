namespace Do.Test.Communication;

public class MockingClients : TestServiceSpec
{
    [Test]
    public async Task Mock_communication_allows_default_response_for_a_client()
    {
        var client = MockMe.TheClient<External>();

        var response = await client.Send(new(UrlOrPath: string.Empty, Method: HttpMethod.Post));

        response.ShouldNotBeNull();
        response.Content.ShouldBe("\"test result\"");  // this reponse result is configured through Communication.Mock feature in TestServiceSpec
    }

    [Test]
    public async Task Mock_communication_allows_conditioned_default_responses_for_a_client()
    {
        var client = MockMe.TheClient<Remote>();

        var response1 = await client.Send(new(UrlOrPath: "path1", Method: HttpMethod.Post));
        response1.ShouldNotBeNull();
        response1.Content.ShouldBe("\"path1 response\"");  // this reponse result is configured through Communication.Mock feature in TestServiceSpec

        var response2 = await client.Send(new(UrlOrPath: "path2", Method: HttpMethod.Post));
        response2.ShouldNotBeNull();
        response2.Content.ShouldBe("\"path2 response\"");  // this reponse result is configured through Communication.Mock feature in TestServiceSpec
    }

    [Test]
    public async Task Response_of_a_client_can_be_set_through_mock_helper()
    {
        var client = MockMe.TheClient<External>(response: "overridden response");

        var response = await client.Send(new(UrlOrPath: string.Empty, Method: HttpMethod.Post));

        response.ShouldNotBeNull();
        response.Content.ShouldBe("\"overridden response\"");
    }
}
