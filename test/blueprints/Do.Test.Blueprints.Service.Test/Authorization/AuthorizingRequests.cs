namespace Do.Test.Authorization;

public class AuthorizingRequests : TestServiceNfr
{
    [Test]
    public async Task Authorizes_succesfully_authenticated_user()
    {
        Client.DefaultRequestHeaders.Authorization = UserFixedBearerToken;

        var response = await Client.PostAsync("authorization-samples/require-base-claim", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Authorizes_succesfully_authenticated_user_with_valid_claim()
    {
        Client.DefaultRequestHeaders.Authorization = AdminFixedBearerToken;

        var response = await Client.PostAsync("authorization-samples/require-admin-claim", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Require_no_claim_authorizes_all_requests()
    {
        Client.DefaultRequestHeaders.Clear();

        var response = await Client.PostAsync("authorization-samples/require-no-claim", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }
}