using Do.Architecture;
using Do.Authentication;
using Do.Authorization;
using System.Net.Http.Headers;

namespace Do.Test.Authorization;

public class AuthorizingRequests : TestServiceNfr
{
    const string ApiKey = "11111111111111111111111111111111";

    protected override IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? Authentications =>
        [c => c.FixedBearerToken(tokens => tokens.Add("Default", claims: ["User", "Admin"]))];
    protected override Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? Authorization =>
        c => c.ClaimBased(baseClaim: "User", claims: ["Admin"]);

    [Test]
    public async Task Authorizes_succesfully_authenticated_user()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(ApiKey);

        var response = await Client.PostAsync("authorization-samples/require-authorization", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Authorizes_succesfully_authenticated_use_with_valid_claim()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(ApiKey);

        var response = await Client.PostAsync("authorization-samples/require-admin-claim", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Require_no_claim_authorizes_all_requests()
    {
        var response = await Client.PostAsync("authorization-samples/require-no-claim", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }
}