using Do.Authorization;
using Do.Authorization.ClaimBased;
using Microsoft.AspNetCore.Authorization;

namespace Do;

public static class ClaimBasedAuthorizationExtensions
{
    public static ClaimBasedAuthorizationFeature ClaimBased(this AuthorizationConfigurator _,
        KeyValuePair<string, Action<AuthorizationPolicyBuilder>>[]? policies = default
    ) => new(policies ?? []);
}