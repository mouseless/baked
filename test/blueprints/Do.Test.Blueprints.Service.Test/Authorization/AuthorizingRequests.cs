using System.Net.Http.Headers;

namespace Do.Test.Authorization;
public class AuthorizingRequests : TestServiceNfr
{
    [Test]
    public async Task Authorizes_succesfully_authenticated_user()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("11111111111111111111111111111111");

        var response = await Client.PostAsync("authorization-samples/require-authorization", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Authorizes_succesfully_authenticated_use_with_valid_policy()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("11111111111111111111111111111111");

        var response = await Client.PostAsync("authorization-samples/require-admin-policy", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }
}
