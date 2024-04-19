namespace Do.Test.Authentication;

public class ValidatingFormPost : TestServiceSpec
{
    [Test]
    public void Concats_given_data_with_token_and_compares_it_to_provided_hash()
    {
        var request = GiveMe.AnHttpRequest(
            form: GiveMe.ADictionary(
                ("hash", "Fp4hWyHBe50ZkackNZdDMIO7My70naTsQUZDoS0v9aw="), // 12token -sha256-> 169E215B21C17B9D1991A7243597433083BB332EF49DA4EC414643A12D2FF5AC
                ("value-1", "1"),
                ("value-2", "2")
            )
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request, tokenNames: ["Test"]);
        MockMe.ASetting(key: "Authentication:FixedToken:Test", value: "token");

        var action = () => handler.AuthenticateAsync();

        action.ShouldNotThrow();
    }

    [Test]
    public void Throws_unauthorized_access_when_hash_and_parameters_does_not_match()
    {
        var request = GiveMe.AnHttpRequest(
           form: GiveMe.ADictionary(
             ("hash", "Fp4hWyHBe50ZkackNZdDMIO7My70naTsQUZDoS0v9aw="), // 12token -sha256-> 169E215B21C17B9D1991A7243597433083BB332EF49DA4EC414643A12D2FF5AC
             ("value-1", "1"),
             ("value-2", "2")
           )
       );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request, tokenNames: ["Test"]);
        MockMe.ASetting(key: "Authentication:FixedToken:Test", value: "other-token");

        var action = () => handler.AuthenticateAsync();

        action.ShouldThrow<UnauthorizedAccessException>();
    }

    [Test]
    public void Throws_unauthorized_access_when_hash_is_not_provided()
    {
        var request = GiveMe.AnHttpRequest(
            form: GiveMe.ADictionary()
        );
        var handler = GiveMe.AFixedBearerTokenAuthenticationHandler(request, tokenNames: ["Test"]);
        MockMe.ASetting(key: "Authentication:FixedToken:Test", value: GiveMe.AString());

        var action = () => handler.AuthenticateAsync();

        action.ShouldThrow<UnauthorizedAccessException>();
    }
}