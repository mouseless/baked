using Do.Architecture;
using Do.Authentication;
using Do.Authorization;
using Newtonsoft.Json;

namespace Do.Test.Authentication;

public class AuthenticatingWithMultipleHandlers : TestServiceNfr
{
    protected override IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? Authentications =>
       [
            c => c.FixedBearerToken(tokens =>
            {
                tokens.Add("Jane", claims: ["User"]);
                tokens.Add("John", claims: ["User", "Admin"]);
            }),
           c => c.ApiKey("Default", claims: ["System"])
        ];
    protected override Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? Authorization =>
        c => c.Disabled();

    [TestCase("Authorization", "11111111111111111111111111111111", "User")]
    [TestCase("X-Api-Key", "Api_Key", "System")]
    public async Task Request_can_be_authenticated_with_available_handler(string header, string value, string claim)
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
    public async Task Request_is_only_handed_by_first_available_handler()
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
    public async Task User_is_not_authenticated_and_have_no_claims_when_no_handler_can_authenticate()
    {
        var response = await Client.GetAsync("authentication-samples/claims");

        response.IsSuccessStatusCode.ShouldBeTrue();

        var content = await response.Content.ReadAsStringAsync();
        var identites = JsonConvert.DeserializeObject<List<IdentityData>>(content);
        identites.ShouldNotBeNull();
        identites.ShouldNotContain(i => i.Claims.Any());
    }

    [TestCase("Authorization", "Wrong_Token")]
    [TestCase("X-Api-Key", "Wrong_Key")]
    public async Task User_is_not_authenticated_and_have_no_claims_when_authentication_fails(string header, string value)
    {
        Client.DefaultRequestHeaders.Add(header, value);

        var response = await Client.GetAsync("authentication-samples/claims");

        response.IsSuccessStatusCode.ShouldBeTrue();

        var content = await response.Content.ReadAsStringAsync();
        var identites = JsonConvert.DeserializeObject<List<IdentityData>>(content);
        identites.ShouldNotBeNull();
        identites.ShouldNotContain(i => i.Claims.Any());
    }
}