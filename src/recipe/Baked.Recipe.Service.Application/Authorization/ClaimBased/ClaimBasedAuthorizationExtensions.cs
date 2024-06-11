using Baked.Authorization;
using Baked.Authorization.ClaimBased;

namespace Baked;

public static class ClaimBasedAuthorizationExtensions
{
    public static ClaimBasedAuthorizationFeature ClaimBased(this AuthorizationConfigurator _,
        IEnumerable<string>? claims = default,
        IEnumerable<string>? baseClaims = default
    )
    {
        claims ??= [];
        baseClaims ??= [];

        return new(claims, baseClaims);
    }
}