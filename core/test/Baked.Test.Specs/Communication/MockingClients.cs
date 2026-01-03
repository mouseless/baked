using Baked.Playground.Communication;
using System.Net;

namespace Baked.Test.Communication;

public class MockingClients : TestSpec
{
    [Test]
    public async Task Mock_communication_allows_default_response_for_a_client()
    {
        var client = MockMe.TheClient<ExternalSamples>();

        var response = await client.Send(new(string.Empty, HttpMethod.Post));

        response.ShouldNotBeNull();
        response.Content.ShouldBe("\"test result\"");  // this reponse result is configured through Communication.Mock feature in TestSpec
    }

    [Test]
    public async Task Mock_communication_allows_conditioned_default_responses_for_a_client()
    {
        var client = MockMe.TheClient<InternalSamples>();

        var response1 = await client.Send(new("path1", HttpMethod.Post));
        response1.ShouldNotBeNull();
        response1.Content.ShouldBe("\"path1 response\"");  // this reponse result is configured through Communication.Mock feature in TestSpec

        var response2 = await client.Send(new("path2", HttpMethod.Post));
        response2.ShouldNotBeNull();
        response2.Content.ShouldBe("\"path2 response\"");  // this reponse result is configured through Communication.Mock feature in TestSpec
    }

    [Test]
    public async Task Response_of_a_client_can_be_set_through_mock_helper()
    {
        var client = MockMe.TheClient<ExternalSamples>(response: "overridden response");

        var response = await client.Send(new(string.Empty, HttpMethod.Post));

        response.ShouldNotBeNull();
        response.Content.ShouldBe("\"overridden response\"");
    }

    [Test]
    public async Task Mock_helper_can_configure_individual_response_per_path()
    {
        MockMe.TheClient<ExternalSamples>(path: "path1", response: new { content = "Response 1" });
        var client = MockMe.TheClient<ExternalSamples>(path: "path2", response: new { content = "Response 2" });

        var response = await client.Send(new("path2", HttpMethod.Post));

        response.Content.ShouldBe(new { content = "Response 2" }.ToJsonString());
    }

    [Test]
    public async Task Mock_helper_can_configure_sequence_of_responses()
    {
        var client = MockMe.TheClient<ExternalSamples>(responses: [new { content = "Response 1" }, new { content = "Response 2" }]);

        var responseOne = await client.Send(new(string.Empty, HttpMethod.Post));
        var responseTwo = await client.Send(new(string.Empty, HttpMethod.Post));

        responseOne.Content.ShouldBe(new { content = "Response 1" }.ToJsonString());
        responseTwo.Content.ShouldBe(new { content = "Response 2" }.ToJsonString());
    }

    [Test]
    public async Task Mock_helper_can_configure_client_to_throw_exception()
    {
        var client = MockMe.TheClient<ExternalSamples>(throws: new Exception());

        var task = client.Send(new(string.Empty, HttpMethod.Post));

        await task.ShouldThrowAsync<Exception>();
    }

    [Test]
    public async Task Mock_helper_can_configures_OK_as_default_response_code()
    {
        var client = MockMe.TheClient<ExternalSamples>(noResponse: true);

        var response = await client.Send(new(string.Empty, HttpMethod.Post));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Mock_helper_can_configure_response_status_code_as_non_success()
    {
        var client = MockMe.TheClient<ExternalSamples>(statusCode: HttpStatusCode.NotFound);

        var response = await client.Send(new(string.Empty, HttpMethod.Post));

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Mock_helper_can_configure_non_success_response_with_error_content()
    {
        var client = MockMe.TheClient<ExternalSamples>(statusCode: HttpStatusCode.BadRequest, responseString: "Invalid Request");

        var response = await client.Send(new(string.Empty, HttpMethod.Post));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        response.Content.ShouldBe("Invalid Request");
    }

    [Test]
    public async Task Mock_helper_does_not_configure_mock_when_no_response_parameters_are_set()
    {
        MockMe.TheClient<ExternalSamples>(responseString: "response");
        var client = MockMe.TheClient<ExternalSamples>();

        var response = await client.Send(new(string.Empty, HttpMethod.Post));

        response.Content.ShouldBe("response");
    }

    [Test]
    public async Task Mock_helper_can_clear_previous_invocations()
    {
        var client = MockMe.TheClient<ExternalSamples>();
        await client.Send(new("path", HttpMethod.Post));
        MockMe.TheClient<ExternalSamples>(clearInvocations: true);

        await client.Send(new("path", HttpMethod.Post));

        client.VerifySent(urlContains: "path", times: 1);
    }
}