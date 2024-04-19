using Do.Authorization;
using Do.Authorization.ClaimBased;

namespace Do;

public static class ClaimBasedAuthorizationExtensions
{
    public static ClaimBasedAuthorizationFeature FixedToken(this AuthorizationConfigurator _) =>
        new();
}
