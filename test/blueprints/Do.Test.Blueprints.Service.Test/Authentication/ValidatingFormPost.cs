using Microsoft.AspNetCore.Authentication;

namespace Do.Test.Authentication;

public class ValidatingFormPost : TestServiceSpec
{
    [Test]
    public async Task Concats_given_data_with_token_and_compares_it_to_provided_hash()
    {
        var request = GiveMe.AnHttpRequest(
            form: GiveMe.ADictionary(
                ("hash", "Fp4hWyHBe50ZkackNZdDMIO7My70naTsQUZDoS0v9aw="), // 12token -sha256-> 169E215B21C17B9D1991A7243597433083BB332EF49DA4EC414643A12D2FF5AC
                ("value-1", "1"),
                ("value-2", "2")
            )
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", ["User"])
        );
        MockMe.ASetting(key: "Authentication:FixedBearerToken:Default", value: "token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeTrue();
        authenticateResult.Principal.ShouldNotBeNull();
        authenticateResult.Principal.Claims.FirstOrDefault(c => c.Type == "User").ShouldNotBeNull();
    }

    [Test]
    public async Task Returns_fail_authenticate_result_when_hash_and_parameters_does_not_match()
    {
        var request = GiveMe.AnHttpRequest(
           form: GiveMe.ADictionary(
             ("hash", "Fp4hWyHBe50ZkackNZdDMIO7My70naTsQUZDoS0v9aw="), // 12token -sha256-> 169E215B21C17B9D1991A7243597433083BB332EF49DA4EC414643A12D2FF5AC
             ("value-1", "1"),
             ("value-2", "2")
           )
       );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
             tokens => tokens.Add("Default", ["User"])
         );
        MockMe.ASetting(key: "Authentication:FixedBearerToken:Default", value: "other-token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeFalse();
        authenticateResult.Failure.ShouldNotBeNull();
        authenticateResult.Failure.ShouldBeAssignableTo<AuthenticationFailureException>();
    }

    [Test]
    public async Task Does_not_validate_request_when_hash_is_not_provided_and_returns_no_result()
    {
        var request = GiveMe.AnHttpRequest(
            form: GiveMe.ADictionary()
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", ["User"])
        );
        MockMe.ASetting(key: "Authentication:FixedBearerToken:Default", value: GiveMe.AString());

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeFalse();
        authenticateResult.Failure.ShouldBeNull();
        authenticateResult.Principal.ShouldBeNull();
    }
}