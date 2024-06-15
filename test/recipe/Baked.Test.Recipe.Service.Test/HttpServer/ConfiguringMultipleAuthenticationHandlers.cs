using System.Net;
using System.Net.Http.Headers;

namespace Baked.Test.HttpServer;

public class ConfiguringMultipleAuthenticationHandlers : TestServiceNfr
{
    [TestCase("Authorization", "token-jane", "\"FixedBearerToken\"")]
    [TestCase("X-Api-Key", "apikey", "\"ApiKey\"")]
    public async Task Request_can_be_forwarded_to_available_handlers(string header, string value, string authenticationType)
    {
        Client.DefaultRequestHeaders.Clear();
        Client.DefaultRequestHeaders.Add(header, value);

        var response = await Client.PostAsync("authentication-samples/authenticate", null);
        var result = await response.Content.ReadAsStringAsync();

        result.ShouldBe(authenticationType);
    }

    [Test]
    public async Task Request_is_only_forwarded_to_first_available_handler_with_given_order()
    {
        Client.DefaultRequestHeaders.Authorization = UserFixedBearerToken;
        Client.DefaultRequestHeaders.Add("X-Api-Key", "apikey");

        var response = await Client.PostAsync("authentication-samples/authenticate", null);
        var result = await response.Content.ReadAsStringAsync();

        result.ShouldBe("\"FixedBearerToken\"");
    }

    [Test]
    public async Task Request_is_not_forwarded_to_other_handlers_when_authentication_fails()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Wrong_token");
        Client.DefaultRequestHeaders.Add("X-Api-Key", "apikey");

        var response = await Client.PostAsync("authentication-samples/authenticate", null);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task Context_user_is_not_authenticated_and_when_no_handler_can_authenticate()
    {
        Client.DefaultRequestHeaders.Clear();

        var response = await Client.PostAsync("authentication-samples/authenticate", null);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}