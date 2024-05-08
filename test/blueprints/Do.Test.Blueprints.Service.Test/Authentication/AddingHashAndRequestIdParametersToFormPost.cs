using System.Net;

namespace Do.Test.Authentication;

public class AddingHashAndRequestIdParametersToFormPost : TestServiceNfr
{
    [Test]
    public async Task RequestId_and_hash_parameters_are_added_to_form_post_authenticate_requests()
    {
        var form = new Dictionary<string, string>
        {
            ["value"] = "value",
            ["requestId"] = "requestId",
            ["hash"] = "VGQCN7lFnTCAf6YvvZjwUNG/w8J2LGnpELlBFP8a8Yw=" // valuerequestId111111111111111111111111 -sha256-> 54640237b9459d30807fa62fbd98f050d1bfc3c2762c69e910b94114ff1af18c
        };

        var response = await Client.PostAsync("authentication-samples/form-post-authenticate", new FormUrlEncodedContent(form));
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task RequestId_parameter_is_optional()
    {
        var form = new Dictionary<string, string>
        {
            ["value"] = "value",
            ["hash"] = "VGQCN7lFnTCAf6YvvZjwUNG/w8J2LGnpELlBFP8a8Yw=" // requestIdvalue111111111111111111111111 -sha256-> 54640237b9459d30807fa62fbd98f050d1bfc3c2762c69e910b94114ff1af18c
        };

        var response = await Client.PostAsync("authentication-samples/form-post-authenticate", new FormUrlEncodedContent(form));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Hash_parameter_is_optional()
    {
        var form = new Dictionary<string, string>
        {
            ["requestId"] = "requestId",
            ["value"] = "value"
        };

        var response = await Client.PostAsync("authentication-samples/form-post-authenticate", new FormUrlEncodedContent(form));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}