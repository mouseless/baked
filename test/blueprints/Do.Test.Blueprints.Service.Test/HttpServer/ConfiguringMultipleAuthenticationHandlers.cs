using Do.Architecture;
using Do.Authentication;

namespace Do.Test.HttpServer;

public class ConfiguringMultipleAuthenticationHandlers : TestServiceNfr
{
    protected override IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? Authentications =>
        [
            c => c.FixedBearerToken(tokens =>
            {
                tokens.Add("Jane");
            }),
            c => c.ApiKey()
        ];

    [TestCase("Authorization", "11111111111111111111111111111111", "FixedBearerToken")]
    [TestCase("X-Api-Key", "apikey", "ApiKey")]
    public async Task Request_can_be_forwarded_to_available_handlers(string header, string value, string authenticationType)
    {
        Client.DefaultRequestHeaders.Add(header, value);

        var response = await Client.PostAsync("authentication-samples/authenticate", null);
        var result = await response.Content.ReadAsStringAsync();

        result.ShouldBe(authenticationType);
    }

    [Test]
    public async Task Request_is_only_forwarded_to_first_available_handler_with_given_order()
    {
        Client.DefaultRequestHeaders.Add("Authorization", "11111111111111111111111111111111");
        Client.DefaultRequestHeaders.Add("X-Api-Key", "apikey");

        var response = await Client.PostAsync("authentication-samples/authenticate", null);
        var result = await response.Content.ReadAsStringAsync();

        result.ShouldBe("FixedBearerToken");
    }

    [Test]
    public async Task Request_is_not_forwarded_to_other_handlers_when_authentication_fails()
    {
        Client.DefaultRequestHeaders.Add("Authorization", "Wrong_token");
        Client.DefaultRequestHeaders.Add("X-Api-Key", "apikey");

        var response = await Client.PostAsync("authentication-samples/authenticate", null);
        var result = await response.Content.ReadAsStringAsync();

        result.ShouldBeNullOrEmpty();
    }

    [Test]
    public async Task Context_user_is_not_authenticated_and_when_no_handler_can_authenticate()
    {
        var response = await Client.PostAsync("authentication-samples/authenticate", null);
        var result = await response.Content.ReadAsStringAsync();

        result.ShouldBeNullOrEmpty();
    }
}