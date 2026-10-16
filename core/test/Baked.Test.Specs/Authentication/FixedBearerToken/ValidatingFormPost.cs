using Microsoft.AspNetCore.Authentication;

namespace Baked.Test.Authentication.FixedBearerToken;

public class ValidatingFormPost : TestSpec
{
    [Test]
    public async Task Signs_given_data_with_token_and_compares_it_to_provided_hash()
    {
        var request = GiveMe.AnHttpRequest(
            form: GiveMe.ADictionary(
                ("hash", "ok8VFArYZ2g8EWFGYWyeLBV7GfoNo8/momU1DxOZ1hs="), // value-1=1&value-2=2 -hmacsha256(token)-> base64
                ("value-1", "1"),
                ("value-2", "2")
            )
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", claims: ["User"])
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
             ("hash", "ok8VFArYZ2g8EWFGYWyeLBV7GfoNo8/momU1DxOZ1hs="), // value-1=1&value-2=2 -hmacsha256(token)-> base64
             ("value-1", "1"),
             ("value-2", "2")
           )
       );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
             tokens => tokens.Add("Default", claims: ["User"])
         );
        MockMe.ASetting(key: "Authentication:FixedBearerToken:Default", value: "other-token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeFalse();
        authenticateResult.Failure.ShouldNotBeNull();
        authenticateResult.Failure.ShouldBeAssignableTo<AuthenticationFailureException>();
    }

    [Test]
    public async Task Fails_when_a_value_is_shifted_to_another_field()
    {
        var request = GiveMe.AnHttpRequest(
            form: GiveMe.ADictionary(
                ("hash", "s6XikSoY+80FU/pqi36U6fR0m7V1K3xPQpbJcA3FNbs="), // amount=12&fee=3 -hmacsha256(token)-> base64
                ("amount", "1"),
                ("fee", "23")
            )
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", claims: ["User"])
        );
        MockMe.ASetting(key: "Authentication:FixedBearerToken:Default", value: "token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeFalse();
    }

    [Test]
    public async Task Order_of_fields_does_not_change_the_hash()
    {
        var request = GiveMe.AnHttpRequest(
            form: GiveMe.ADictionary(
                ("fee", "3"),
                ("hash", "s6XikSoY+80FU/pqi36U6fR0m7V1K3xPQpbJcA3FNbs="), // amount=12&fee=3 -hmacsha256(token)-> base64
                ("amount", "12")
            )
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", claims: ["User"])
        );
        MockMe.ASetting(key: "Authentication:FixedBearerToken:Default", value: "token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeTrue();
    }

    [Test]
    public async Task Keys_and_values_are_url_encoded_before_signing()
    {
        var request = GiveMe.AnHttpRequest(
            form: GiveMe.ADictionary(
                ("hash", "c0C1X4Nfa0jreAUfk9QHTJU26hrzit1csFNpnR3mb3E="), // name=a%20b%26c%3Dd -hmacsha256(token)-> base64
                ("name", "a b&c=d")
            )
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", claims: ["User"])
        );
        MockMe.ASetting(key: "Authentication:FixedBearerToken:Default", value: "token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeTrue();
    }

    [Test]
    public async Task Hash_is_ignored_when_authorization_header_is_given()
    {
        var request = GiveMe.AnHttpRequest(
            header: GiveMe.ADictionary([("Authorization", "Bearer header_token")]),
            form: GiveMe.ADictionary(
                ("hash", "invalid-hash"),
                ("amount", "12")
            )
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens =>
            {
                tokens.Add("Header", claims: ["HeaderClaim"]);
                tokens.Add("Form", claims: ["FormClaim"]);
            }
        );
        MockMe.ASetting(key: "Authentication:FixedBearerToken:Header", value: "header_token");
        MockMe.ASetting(key: "Authentication:FixedBearerToken:Form", value: "token");

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeTrue();
        authenticateResult.Principal.ShouldNotBeNull();
        authenticateResult.Principal.Claims.FirstOrDefault(c => c.Type == "HeaderClaim").ShouldNotBeNull();
    }

    [Test]
    public async Task Does_not_validate_request_when_hash_is_not_provided_and_returns_no_result()
    {
        var request = GiveMe.AnHttpRequest(
            form: GiveMe.ADictionary()
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request,
            tokens => tokens.Add("Default", claims: ["User"])
        );
        MockMe.ASetting(key: "Authentication:FixedBearerToken:Default", value: GiveMe.AString());

        var authenticateResult = await handler.AuthenticateAsync();

        authenticateResult.Succeeded.ShouldBeFalse();
        authenticateResult.Failure.ShouldBeNull();
        authenticateResult.Principal.ShouldBeNull();
    }
}