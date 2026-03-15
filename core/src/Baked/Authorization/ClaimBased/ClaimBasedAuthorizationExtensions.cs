using Baked.Authorization;
using Baked.Authorization.ClaimBased;

namespace Baked;

public static class ClaimBasedAuthorizationExtensions
{
    extension(AuthorizationConfigurator _)
    {
        public ClaimBasedAuthorizationFeature ClaimBased(
            IEnumerable<string>? claims = default,
            IEnumerable<string>? baseClaims = default
        )
        {
            claims ??= [];
            baseClaims ??= [];

            return new(claims, baseClaims);
        }
    }
}