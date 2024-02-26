using Do.Architecture;
using Do.Authentication;
using System.Net.Http.Headers;

namespace Do.Test.Authentication;

public class AuthenticatingRequests : TestServiceNfr
{
    protected override Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>? Authentication =>
        c => c.FixedToken(["Backend"]);

    [Test]
    public async Task Validates_request_using_Authorization_header()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("11111111111111111111111111111111");

        var response = await Client.GetAsync("singleton/time");
        ((int)response.StatusCode).ShouldBe(expected: 200, customMessage: response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
    }

    [Test]
    public async Task Ignores_bearer_when_validating_header_value()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer 11111111111111111111111111111111");

        var response = await Client.GetAsync("singleton/time");
        ((int)response.StatusCode).ShouldBe(expected: 200, customMessage: response.ReasonPhrase);
    }

    [Test]
    public async Task Does_not_return_success_status_if_token_is_not_valid()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("21111111111111111111111111111111");

        var response = await Client.GetAsync("singleton/time");
        ((int)response.StatusCode).ShouldNotBe(200);
    }
}
