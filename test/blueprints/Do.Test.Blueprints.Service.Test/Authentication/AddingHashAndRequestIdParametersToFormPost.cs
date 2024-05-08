using System.Net;

namespace Do.Test.Authentication;

public class AddingHashAndRequestIdParametersToFormPost : TestServiceNfr
{
    [Test]
    public async Task RequestId_and_hash_parameters_are_added_to_form_post_authenticate_requests()
    {
        var form = new Dictionary<string, string>
        {
            ["requestId"] = "requestId",
            ["value"] = "value",
            ["hash"] = "KtvYUkJPIhwki0EsfAqAI+i4FQyEtwPbK+EifUcocos=" // requestIdvalue111111111111111111111111 -sha256-> 2adbd852424f221c248b412c7c0a8023e8b8150c84b703db2be1227d4728728b
        };

        var response = await Client.PostAsync("authentication-samples/form-post-authenticate", new FormUrlEncodedContent(form));
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task RequestId_parameter_is_required()
    {
        var form = new Dictionary<string, string>
        {
            ["value"] = "value",
            ["hash"] = "KtvYUkJPIhwki0EsfAqAI+i4FQyEtwPbK+EifUcocos=" // requestIdvalue111111111111111111111111 -sha256-> 2adbd852424f221c248b412c7c0a8023e8b8150c84b703db2be1227d4728728b
        };

        var response = await Client.PostAsync("authentication-samples/form-post-authenticate", new FormUrlEncodedContent(form));
        var content = await response.Content.ReadAsStringAsync();

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        content.ShouldContain("The requestId field is required.");
    }

    [Test]
    public async Task Hash_parameter_is_required()
    {
        var form = new Dictionary<string, string>
        {
            ["requestId"] = "requestId",
            ["value"] = "value"
        };

        var response = await Client.PostAsync("authentication-samples/form-post-authenticate", new FormUrlEncodedContent(form));
        var content = await response.Content.ReadAsStringAsync();

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        content.ShouldContain("The hash field is required.");
    }
}