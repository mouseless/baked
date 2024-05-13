namespace Do.Test.Authentication;

public class ValidatingAuthorizationHeader : TestServiceSpec
{
    [TestCase("token_a")]
    [TestCase("token_b")]
    public async Task Validates_given_bearer_token_in_configured_tokens(string token)
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", token))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
             tokens =>
             {
                 tokens.Add("A", ["ClaimA"]);
                 tokens.Add("B", ["ClaimB"]);
             });
        MockMe.ASetting("Authentication:FixedBearerToken:A", "token_a");
        MockMe.ASetting("Authentication:FixedBearerToken:B", "token_b");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.ShouldBeSuccededResult();
    }

    [Test]
    public async Task Returns_failure_when_provided_token_does_not_match_any_configured_token()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", "Bearer wrong_token"))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
             tokens => tokens.Add("Default", ["User"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Default", "test_token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.ShouldBeFailedResult();
    }

    [Test]
    public async Task Trims_bearer_scheme_and_whitespace()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", "Bearer test_token "))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", ["User"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Default", "test_token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.ShouldBeSuccededResult();
    }
}