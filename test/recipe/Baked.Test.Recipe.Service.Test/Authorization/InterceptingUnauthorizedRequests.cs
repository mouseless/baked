using System.Net;
using System.Net.Http.Headers;

namespace Baked.Test.Authorization;

public class InterceptingUnauthorizedRequests : TestServiceNfr
{
    [Test]
    public async Task Returns_unauthorized_access_response_for_not_authenticated_user()
    {
        Client.DefaultRequestHeaders.Clear();

        var response = await Client.PostAsync("authorization-samples/authenticated", null);

        response.IsSuccessStatusCode.ShouldBeFalse();
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task Returns_unauthorized_access_response_for_failed_authenticatation()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Wrong_token");

        var response = await Client.PostAsync("authorization-samples/user", null);

        response.IsSuccessStatusCode.ShouldBeFalse();
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task Returns_forbidden_response_when_claim_requirements__are_not_met()
    {
        Client.DefaultRequestHeaders.Authorization = UserFixedBearerToken;

        var response = await Client.PostAsync("authorization-samples/admin", null);

        response.IsSuccessStatusCode.ShouldBeFalse();
        response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
    }
}