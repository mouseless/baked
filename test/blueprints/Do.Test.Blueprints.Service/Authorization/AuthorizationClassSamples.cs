using Do.Authorization;
using System.Security.Claims;

namespace Do.Test.Authorization;

[RequireUser(["GivenA", "GivenB"])]
public class AuthorizationClassSamples(Func<ClaimsPrincipal> _getClaims)
    : AuthorizationSamplesBase(_getClaims)
{
    public string ClassClaims() =>
        Validate(["GivenA", "GivenB", "BaseA", "BaseB"]);

    [RequireUser(["GivenC"], Override = true)]
    public string MethodOverClassClaims() =>
        Validate(["GivenC"]);
}