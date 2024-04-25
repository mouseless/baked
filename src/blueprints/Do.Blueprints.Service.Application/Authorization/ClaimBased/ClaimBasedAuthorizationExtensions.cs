using Do.Authorization;
using Do.Authorization.ClaimBased;

namespace Do;

public static class ClaimBasedAuthorizationExtensions
{
    public static ClaimBasedAuthorizationFeature ClaimBased(this AuthorizationConfigurator _,
        string? baseClaim = default,
        List<string>? claims = default
    ) => new(baseClaim, claims ?? []);
}