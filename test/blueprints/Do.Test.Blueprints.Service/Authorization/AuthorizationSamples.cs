using Do.Authorization;
using System.Security.Claims;

namespace Do.Test.Authorization;

public class AuthorizationSamples(Func<ClaimsPrincipal> _getClaims)
{
    public Dictionary<string, string> RequireBaseClaim() =>
        _getClaims().Claims.ToDictionary(c => c.Type, c => c.Value);

    [RequireClaim("Admin")]
    public Dictionary<string, string> RequireAdminClaim() =>
        _getClaims().Claims.ToDictionary(c => c.Type, c => c.Value);

    [RequireNoClaim]
    public Dictionary<string, string> RequireNoClaim() =>
        _getClaims().Claims.ToDictionary(c => c.Type, c => c.Value);
}