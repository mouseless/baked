using Do.Architecture;
using Do.Authentication;

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

    [TestCase("Authorization", "11111111111111111111111111111111", "User")]
    [TestCase("X-Api-Key", "Api_Key", "System")]
    public async Task Request_is_authenticated_with_matching_handler(string header, string value, string claim)
    {
        Client.DefaultRequestHeaders.Clear();
        Client.DefaultRequestHeaders.Add(header, value);

        var response = await Client.PostAsync("authentication-samples/token-authentication", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain(claim);
    }

    [Test]
    public async Task Request_is_only_handed_by_first_matching_handler()
    {
        Client.DefaultRequestHeaders.Clear();
        Client.DefaultRequestHeaders.Add("Authorization", "11111111111111111111111111111111");
        Client.DefaultRequestHeaders.Add("X-Api-Key", "Api_Key");

        var response = await Client.PostAsync("authentication-samples/token-authentication", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldNotContain("System");
    }
}