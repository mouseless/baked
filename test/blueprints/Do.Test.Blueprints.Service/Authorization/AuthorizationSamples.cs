using Do.Authorization;
using System.Security.Claims;

namespace Do.Test.Authorization;

public class AuthorizationSamples(Func<ClaimsPrincipal> _getClaims)
{
    public string RequireBaseClaim() =>
        _getClaims().FindFirst("User")?.Value ?? throw new("User claim should have existed");

    [RequireClaim("Admin")]
    public string RequireAdminClaim() =>
        _getClaims().FindFirst("Admin")?.Value ?? throw new("Admin claim should have existed");

    [RequireNoClaim]
    public void RequireNoClaim() { }
}