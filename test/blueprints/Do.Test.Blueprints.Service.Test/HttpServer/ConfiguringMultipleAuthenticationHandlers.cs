using Do.Architecture;
using Do.Authentication;
using Do.Authorization;
using Do.Test.Authentication;
using Newtonsoft.Json;

namespace Do.Test.HttpServer;

public class ConfiguringMultipleAuthenticationHandlers : TestServiceNfr
{
    protected override IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? Authentications =>
        [
            c => c.FixedBearerToken(tokens =>
            {
                tokens.Add("Jane", claims: ["User"]);
                tokens.Add("John", claims: ["User", "Admin"]);
            }),
            c => c.ApiKey()
        ];
    protected override Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? Authorization =>
        c => c.Disabled();

    [TestCase("Authorization", "11111111111111111111111111111111", "User")]
    [TestCase("X-Api-Key", "Api_Key", "System")]
    public async Task Request_can_be_forwarded_to_available_handlers(string header, string value, string claim)
    {
        Client.DefaultRequestHeaders.Add(header, value);

        var response = await Client.GetAsync("authentication-samples/claims");

        response.IsSuccessStatusCode.ShouldBeTrue();

        var content = await response.Content.ReadAsStringAsync();
        var identites = JsonConvert.DeserializeObject<List<IdentityData>>(content);
        identites.ShouldNotBeNull();
        identites.ShouldContain(i => i.Claims.Any(c => c.Type == claim));
    }

    [Test]
    public async Task Request_is_only_forwarded_to_first_available_handler_with_given_order()
    {
        Client.DefaultRequestHeaders.Add("Authorization", "11111111111111111111111111111111");
        Client.DefaultRequestHeaders.Add("X-Api-Key", "Api_Key");

        var response = await Client.GetAsync("authentication-samples/claims");

        response.IsSuccessStatusCode.ShouldBeTrue();

        var content = await response.Content.ReadAsStringAsync();
        var identites = JsonConvert.DeserializeObject<List<IdentityData>>(content);
        identites.ShouldNotBeNull();
        identites.ShouldNotContain(i => i.Claims.Any(c => c.Type == "System"));
    }

    [Test]
    public async Task Request_is_not_forwarded_to_other_handlers_when_authentication_fails()
    {
        Client.DefaultRequestHeaders.Add("Authorization", "Wrong_token");
        Client.DefaultRequestHeaders.Add("X-Api-Key", "Api_Key");

        var response = await Client.GetAsync("authentication-samples/claims");

        response.IsSuccessStatusCode.ShouldBeTrue();

        var content = await response.Content.ReadAsStringAsync();
        var identites = JsonConvert.DeserializeObject<List<IdentityData>>(content);
        identites.ShouldNotBeNull();
        identites.ShouldNotContain(i => i.Claims.Any());
    }

    [Test]
    public async Task Context_user_is_not_authenticated_and_have_no_claims_when_no_handler_can_authenticate()
    {
        var response = await Client.GetAsync("authentication-samples/claims");

        response.IsSuccessStatusCode.ShouldBeTrue();

        var content = await response.Content.ReadAsStringAsync();
        var identites = JsonConvert.DeserializeObject<List<IdentityData>>(content);
        identites.ShouldNotBeNull();
        identites.ShouldNotContain(i => i.Claims.Any());
    }
}