using Baked.Authorization;
using System.Security.Claims;

namespace Baked.Test.Authorization;

public class AuthorizationSamples(Func<ClaimsPrincipal> _getClaims)
    : AuthorizationSamplesBase(_getClaims)
{
    [AllowAnonymous]
    public void Anonymous() { }

    [RequireUser(["Admin"])]
    public string Admin() =>
        Validate(["Admin"]);

    [RequireUser(["User"])]
    public string User() =>
        Validate(["User"]);

    [RequireUser(Override = true)]
    public string Authenticated() =>
        Validate([]);

    public string BaseClaims() =>
        Validate(["BaseA", "BaseB"]);

    [RequireUser(["GivenA", "GivenB"], Override = true)]
    public string GivenClaims() =>
        Validate(["GivenA", "GivenB"]);

    [RequireUser(["GivenA", "GivenB"])]
    public string GivenAndBaseClaims() =>
        Validate(["GivenA", "GivenB", "BaseA", "BaseB"]);
}