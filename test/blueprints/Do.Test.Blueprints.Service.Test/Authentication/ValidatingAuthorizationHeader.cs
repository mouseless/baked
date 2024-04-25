using Microsoft.AspNetCore.Authentication;

namespace Do.Test.Authentication;

public class ValidatingAuthorizationHeader : TestServiceSpec
{
    [TestCase("token_a", "ClaimA")]
    [TestCase("token_b", "ClaimB")]
    public async Task Validates_given_bearer_token_in_configured_tokens(string token, string claim)
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

        authenticateResult.Succeeded.ShouldBeTrue();
        authenticateResult.Principal.ShouldNotBeNull();
        authenticateResult.Principal.Claims.FirstOrDefault(c => c.Type == claim).ShouldNotBeNull();
    }

    [Test]
    public async Task Returns_when_provided_token_does_not_match_any_fixed_token()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", "Bearer wrong_token"))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
             tokens => tokens.Add("Test", ["User"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Test", "test_token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeFalse();
        authenticateResult.Failure.ShouldNotBeNull();
        authenticateResult.Failure.ShouldBeAssignableTo<AuthenticationFailureException>();
    }

    [Test]
    public async Task Trims_bearer_scheme_and_whitespace()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", "Bearer test_token "))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Test", ["User"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Test", "test_token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeTrue();
        authenticateResult.Principal.ShouldNotBeNull();
        authenticateResult.Principal.Claims.FirstOrDefault(c => c.Type == "User").ShouldNotBeNull();
    }
}