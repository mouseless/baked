namespace Baked.Test.Authentication.Jwt;

public class BuildingToken : TestServiceSpec
{
    [Test]
    public void Creates_token_with_given_claims_and_jwt_settings()
    {
        MockMe.TheJwtSettings(issuer: "Iss", audience: "Aud");
        MockMe.TheTime(new(2025, 03, 25, 16, 30, 0));
        var tokenBuilder = GiveMe.AJwtTokenBuilder();

        var token = tokenBuilder.Build("Access", [new("User", "User")]);

        var access = GiveMe.TheSecurityToken(token);
        access.Claims.ShouldContain(c => c.Type == "User" && c.Value == "User");
        access.Payload.Iss.ShouldBe("Iss");
        access.Payload.Aud.ShouldContain("Aud");
    }

    [Test]
    public void Token_expiration_can_be_different_for_access_and_refresg()
    {
        MockMe.TheJwtSettings(expirationForAccess: 5, expirationForRefresh: 15);
        MockMe.TheTime(new(2025, 03, 25, 16, 30, 0));
        var tokenBuilder = GiveMe.AJwtTokenBuilder();

        var token = tokenBuilder.Build("Access", []);

        var access = GiveMe.TheSecurityToken(token);
        access.Payload.IssuedAt.ToLocalTime().ShouldBe(DateTime.Parse("2025-03-25T16:30:00"));
        access.Payload.ValidTo.ToLocalTime().ShouldBe(DateTime.Parse("2025-03-25T16:35:00"));

        token = tokenBuilder.Build("Refresh", []);

        var refresh = GiveMe.TheSecurityToken(token);
        refresh.Payload.IssuedAt.ToLocalTime().ShouldBe(DateTime.Parse("2025-03-25T16:30:00"));
        refresh.Payload.ValidTo.ToLocalTime().ShouldBe(DateTime.Parse("2025-03-25T16:45:00"));
    }
}