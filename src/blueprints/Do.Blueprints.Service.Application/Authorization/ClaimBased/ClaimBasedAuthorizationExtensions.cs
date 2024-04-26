using Do.Authorization;
using Do.Authorization.ClaimBased;

namespace Do;

public static class ClaimBasedAuthorizationExtensions
{
    public static ClaimBasedAuthorizationFeature ClaimBased(this AuthorizationConfigurator _,
        IEnumerable<string>? claims = default,
        string? baseClaim = default
    )
    {
        claims ??= ["User"];

        return new(claims, baseClaim);
    }
}