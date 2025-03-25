using Baked.Authentication;

namespace Baked.Test.Authentication.Jwt;

public class BuildingToken : TestServiceSpec
{
    public void Creates_token_with_given_claims_and_jwt_settings()
    {
        MockMe.TheJwtSettings(issuer: "Iss", audience: "Aud", expirationForAccess: 5);
        MockMe.TheTime(new(2025, 03, 25, 16, 30, 0));
        var tokenBuilder = GiveMe.The<ITokenBuilder>();

        var token = tokenBuilder.Build("Jwt", [new("User", "User")]);

        var access = GiveMe.TheSecurityToken(token);
        access.Claims.ShouldContain(c => c.Type == "User" && c.Value == "User");
        access.Payload.Iss.ShouldBe("Iss");
        access.Payload.Aud.ShouldContain("Aud");
        access.Payload.IssuedAt.ToLocalTime().ShouldBe(DateTime.Parse("2025-03-25T16:30:00"));
        access.Payload.ValidTo.ToLocalTime().ShouldBe(DateTime.Parse("2025-03-25T16:05:00"));
    }
}