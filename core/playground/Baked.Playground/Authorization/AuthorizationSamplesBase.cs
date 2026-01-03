using System.Security.Claims;

namespace Baked.Playground.Authorization;

public abstract class AuthorizationSamplesBase(Func<ClaimsPrincipal> _getClaims)
{
    protected string Validate(string[] expectedClaims)
    {
        var claims = _getClaims();
        if (!expectedClaims.All(ec => claims.HasClaim(ec)))
        {
            throw new($"{expectedClaims.Join(", ")} claims should've existed");
        }

        return claims.Identity?.Name ?? throw new("Identity should have existed");
    }
}