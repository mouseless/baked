using Do.Architecture;
using Do.Authentication;
using Do.Authentication.FixedToken;
using Do.Authorization;
using System.Net.Http.Headers;

namespace Do.Test.Authorization;

public class AuthorizingRequests : TestServiceNfr
{
    const string ApiKey = "11111111111111111111111111111111";

    protected override IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? Authentications =>
        [c => c.FixedToken(configure: options => options.AddIdentity("Admin", [new TokenClaimProvider()]))];
    protected override Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? Authorization =>
        c => c.ClaimBased(policies: [new("AdminOnly", policy => policy.RequireClaim("Token"))]);

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