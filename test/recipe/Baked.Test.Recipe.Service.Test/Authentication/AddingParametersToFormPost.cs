using System.Net;

namespace Do.Test.Authentication;

public class AddingParametersToFormPost : TestServiceNfr
{
    [Test]
    public async Task Hash_and_configured_additional_parameters_are_added_to_form_post_authenticate_requests()
    {
        Client.DefaultRequestHeaders.Clear();

        var form = new Dictionary<string, string>
        {
            ["value"] = "value",
            ["additional"] = "additional",
            ["hash"] = "LqGNbYhL3cCp5vNClVJ3I7D6kCe7rSnb/Tzhp/tV2As=" // valueadditionaltoken-jane -sha256-> 2ea18d6d884bddc0a9e6f34295527723b0fa9027bbad29dbfd3ce1a7fb55d80b
        };

        var response = await Client.PostAsync("authentication-samples/form-post-authenticate", new FormUrlEncodedContent(form));

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldBe("FixedBearerToken");
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