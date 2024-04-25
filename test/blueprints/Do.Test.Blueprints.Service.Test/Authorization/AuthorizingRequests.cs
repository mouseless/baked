using Do.Architecture;
using Do.Authentication;
using Do.Authorization;
using System.Net.Http.Headers;

namespace Do.Test.Authorization;

public class AuthorizingRequests : TestServiceNfr
{
    protected override IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? Authentications =>
        [c => c.FixedBearerToken(tokens =>
            {
                tokens.Add("Default", claims: ["User"]);
                tokens.Add("System", claims: ["System"]);
                tokens.Add("Admin", claims: ["User", "System", "Admin"]);
            })
        ];
    protected override Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? Authorization =>
        c => c.ClaimBased(baseClaim: "User", claims: ["System", "Admin"]);

    [Test]
    public async Task Authorizes_succesfully_authenticated_user()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("11111111111111111111111111111111");

        var response = await Client.PostAsync("authorization-samples/require-base-claim", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [TestCase("22222222222222222222222222222222")]
    [TestCase("33333333333333333333333333333333")]
    public async Task Authorizes_succesfully_authenticated_user_with_valid_claim(string token)
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);

        var response = await Client.PostAsync("authorization-samples/require-system-claim", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Require_no_claim_authorizes_all_requests()
    {
        var response = await Client.PostAsync("authorization-samples/require-no-claim", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }
}