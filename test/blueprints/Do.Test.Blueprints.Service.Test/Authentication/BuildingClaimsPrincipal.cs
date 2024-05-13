namespace Do.Test.Authentication;

public class BuildingClaimsPrincipal : TestServiceSpec
{
    [Test]
    public async Task Builds_claims_principal_from_token_when_authentication_succeeds()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", "test_token "))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", claims: ["User"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Default", "test_token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.ShouldBeSuccededResult(claims: ["User"]);
    }

    [Test]
    public async Task Token_can_have_multiple_claims()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", "test_token"))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", claims: ["User", "Admin"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Default", "test_token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.ShouldBeSuccededResult(claims: ["User", "Admin"]);
    }

    [Test]
    public async Task Does_not_build_claims_principal_from_token_when_authentication_fails()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", "wrong_token"))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", claims: ["User"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Default", "test_token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.ShouldBeFailedResult();
    }
}