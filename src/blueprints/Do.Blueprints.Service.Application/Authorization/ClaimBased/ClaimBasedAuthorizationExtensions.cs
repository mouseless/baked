using Do.Authorization;
using Do.Authorization.ClaimBased;

namespace Do;

public static class ClaimBasedAuthorizationExtensions
{
    public static ClaimBasedAuthorizationFeature ClaimBased(this AuthorizationConfigurator _,
        IEnumerable<string>? claims = default,
        IEnumerable<string>? baseClaims = default
    )
    {
        claims ??= ["User"];
        baseClaims ??= [];

        return new(claims, baseClaims);
    }
}