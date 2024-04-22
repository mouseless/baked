using System.Net;
using System.Net.Http.Headers;

namespace Do.Test.Authorization;

public class InterceptingUnauthorizedRequests : TestServiceNfr
{
    [Test]
    public async Task Returns_unauthorized_access_response_for_invalid_authorization_header()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Wrong_token");

        var response = await Client.PostAsync("authorization-samples/require-authorization", null);

        response.IsSuccessStatusCode.ShouldBeFalse();
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task Returns_unauthorized_access_response_for_invalid_hash_with_form_parameters_and_token()
    {
        var content = new FormUrlEncodedContent([
            // Default: 11111111111111111111111111111111
            // InvalidHash: 12token -sha256-> 169E215B21C17B9D1991A7243597433083BB332EF49DA4EC414643A12D2FF5AC
            new("hash", "Fp4hWyHBe50ZkackNZdDMIO7My70naTsQUZDoS0v9aw="),
            new("value", "1")
        ]);

        var response = await Client.PostAsync("authorization-samples/require-authorization", content);

        response.IsSuccessStatusCode.ShouldBeFalse();
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task Returns_forbidden_response_for_invalid_policy()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("11111111111111111111111111111111");

        var response = await Client.PostAsync("authorization-samples/claim-based-authorization", null);

        response.IsSuccessStatusCode.ShouldBeFalse();
        response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
    }
}