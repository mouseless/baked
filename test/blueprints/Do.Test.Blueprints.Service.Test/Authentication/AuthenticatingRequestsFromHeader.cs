using Do.Authentication;

namespace Do.Test.Authentication;

public class AuthenticatingRequestsFromHeader : TestServiceSpec
{
    [TestCase("token_a")]
    [TestCase("token_b")]
    public void Validates_given_bearer_token_in_configured_tokens(string token)
    {
        var middleware = GiveMe.AFixedTokenMiddleware(tokenNames: ["A", "B"]);
        MockMe.ASetting("Authentication:FixedToken:A", "token_a");
        MockMe.ASetting("Authentication:FixedToken:B", "token_b");
        var request = GiveMe.AnHttpRequest(
            metadata: [new UseAttribute<Do.Authentication.FixedToken.Middleware>()],
            header: GiveMe.ADictionary(("Authorization", token))
        );

        var action = () => middleware.Invoke(request.HttpContext);

        action.ShouldNotThrow();
    }

    [Test]
    public void Throws_unauthorized_access_when_provided_token_does_not_match_any_fixed_token()
    {
        var middleware = GiveMe.AFixedTokenMiddleware(tokenNames: ["Test"]);
        MockMe.ASetting("Authentication:FixedToken:Test", "test_token");
        var request = GiveMe.AnHttpRequest(
            metadata: [new UseAttribute<Do.Authentication.FixedToken.Middleware>()],
            header: GiveMe.ADictionary(("Authorization", "Bearer wrong_token"))
        );

        var action = () => middleware.Invoke(request.HttpContext);

        action.ShouldThrow<UnauthorizedAccessException>();
    }

    [Test]
    public void Trims_bearer_scheme_and_whitespace()
    {
        var middleware = GiveMe.AFixedTokenMiddleware(tokenNames: ["Test"]);
        MockMe.ASetting("Authentication:FixedToken:Test", "test_token");
        var request = GiveMe.AnHttpRequest(
            metadata: [new UseAttribute<Do.Authentication.FixedToken.Middleware>()],
            header: GiveMe.ADictionary(("Authorization", "Bearer test_token "))
        );

        var action = () => middleware.Invoke(request.HttpContext);

        action.ShouldNotThrow();
    }
}
