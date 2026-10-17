using System.Net;

namespace Baked.Test.Authentication.FixedBearerToken;

public class AddingParametersToFormPost : TestNfr
{
    [Test]
    public async Task Hash_and_configured_additional_parameters_are_added_to_form_post_authenticate_requests()
    {
        Client.DefaultRequestHeaders.Clear();

        var form = new Dictionary<string, string>
        {
            ["value"] = "value",
            ["additional"] = "additional",
            ["hash"] = "r5ekgWa9SrYhArw66Rv2ptfZRbYxMGvdYqH9QFJfKjY=" // additional=additional&value=value -hmacsha256(token-jane)-> base64
        };

        var response = await Client.PostAsync("authentication-samples/form-post-authenticate", new FormUrlEncodedContent(form));

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldBe("\"FixedBearerToken\"");
    }

    [Test]
    public async Task Hash_parameter_is_required()
    {
        Client.DefaultRequestHeaders.Clear();

        var form = new Dictionary<string, string>
        {
            ["value"] = "value",
            ["additional"] = "additional"
        };

        var response = await Client.PostAsync("authentication-samples/form-post-authenticate", new FormUrlEncodedContent(form));

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}