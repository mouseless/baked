using System.Security.Claims;

namespace Baked;

public static class AuthorizationExtensions
{
    extension(ClaimsPrincipal claims)
    {
        public bool HasClaim(string claimType) =>
            claims.HasClaim(c => c.Type == claimType);
    }
}