using Do.Architecture;
using Do.Authorization;
using System.Net.Http.Headers;

namespace Do.Test.Authorization;

public class AuthorizingRequests : TestServiceNfr
{
    protected override Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? Authorization =>
        c => c.ClaimBased(claims: ["User", "Admin"], baseClaim: "User");

    public override async Task OneTimeTearDown()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("11111111111111111111111111111111");

        await base.OneTimeTearDown();
    }

    [Test]
    public async Task Authorizes_succesfully_authenticated_user()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("11111111111111111111111111111111");

        var response = await Client.PostAsync("authorization-samples/require-base-claim", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Authorizes_succesfully_authenticated_user_with_valid_claim()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("22222222222222222222222222222222");

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