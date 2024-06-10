namespace Do.Test.Authorization;

public class AuthorizingRequests : TestServiceNfr
{
    [Test]
    public async Task Authorizes_authenticated_user()
    {
        Client.DefaultRequestHeaders.Authorization = GetFixedBearerToken("Authenticated");

        var response = await Client.PostAsync("authorization-samples/authenticated", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Authorizes_base_claims()
    {
        Client.DefaultRequestHeaders.Authorization = GetFixedBearerToken("BaseClaims");

        var response = await Client.PostAsync("authorization-samples/base-claims", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Authorizes_given_claims()
    {
        Client.DefaultRequestHeaders.Authorization = GetFixedBearerToken("GivenClaims");

        var response = await Client.PostAsync("authorization-samples/given-claims", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Authorizes_given_and_base_claims()
    {
        Client.DefaultRequestHeaders.Authorization = GetFixedBearerToken("GivenAndBaseClaims");

        var response = await Client.PostAsync("authorization-samples/given-and-base-claims", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Authorizes_anonymous_user()
    {
        Client.DefaultRequestHeaders.Clear();

        var response = await Client.PostAsync("authorization-samples/anonymous", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Authorizes_class_claims()
    {
        Client.DefaultRequestHeaders.Authorization = GetFixedBearerToken("ClassClaims");

        var response = await Client.PostAsync("authorization-class-samples/class-claims", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Test]
    public async Task Authorizes_method_claims_over_class_claims()
    {
        Client.DefaultRequestHeaders.Authorization = GetFixedBearerToken("MethodOverClassClaims");

        var response = await Client.PostAsync("authorization-class-samples/method-over-class-claims", null);

        response.IsSuccessStatusCode.ShouldBeTrue();
    }
}