using Microsoft.AspNetCore.Authentication;

namespace Baked.Test.Authentication.FixedBearerToken;

public class ValidatingAuthorizationHeader : TestSpec
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
                 tokens.Add("A", claims: ["ClaimA"]);
                 tokens.Add("B", claims: ["ClaimB"]);
             });
        MockMe.ASetting("Authentication:FixedBearerToken:A", "token_a");
        MockMe.ASetting("Authentication:FixedBearerToken:B", "token_b");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeTrue();
    }

    [Test]
    public async Task Returns_failure_when_provided_token_does_not_match_any_configured_token()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", "Bearer wrong_token"))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
             tokens => tokens.Add("Default", claims: ["User"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Default", "test_token");

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
            tokens => tokens.Add("Default", claims: ["User"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Default", "test_token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeTrue();
    }

    [Test]
    public async Task Bearer_scheme_is_case_insensitive()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", "bearer test_token"))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", claims: ["User"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Default", "test_token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeTrue();
    }

    [Test]
    public async Task Only_leading_bearer_scheme_is_trimmed()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", "Bearer test_Bearer_token"))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", claims: ["User"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Default", "test_Bearer_token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeTrue();
    }

    [Test]
    public async Task Empty_configured_tokens_are_ignored()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary(("Authorization", "Bearer "))
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", claims: ["User"])
        );
        MockMe.ASetting("Authentication:FixedBearerToken:Default", string.Empty);

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeFalse();
    }
}