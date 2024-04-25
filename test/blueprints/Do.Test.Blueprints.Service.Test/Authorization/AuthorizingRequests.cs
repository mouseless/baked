using Do.Architecture;
using Do.Authentication;
using Do.Authorization;
using System.Net.Http.Headers;

namespace Do.Test.Authorization;

public class AuthorizingRequests : TestServiceNfr
{
    const string ApiKey = "11111111111111111111111111111111";

    protected override IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? Authentications =>
    [
        c => c.FixedBearerToken(tokens => tokens.Add("Default", claims: ["User", "Admin"]))
    ];

    protected override Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? Authorization =>
        c => c.ClaimBased(["User", "Admin"]);

    [Test]
    public async Task Authorizes_succesfully_authenticated_user()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(ApiKey);

        var response = await Client.PostAsync("authorization-samples/require-authorization", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Authorizes_succesfully_authenticated_use_with_valid_policy()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(ApiKey);

        var response = await Client.PostAsync("authorization-samples/require-admin-policy", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }
}