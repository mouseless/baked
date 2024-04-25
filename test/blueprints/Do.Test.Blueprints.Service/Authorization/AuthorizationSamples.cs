using Do.Authorization;

namespace Do.Test.Authorization;

public class AuthorizationSamples
{
    public void RequireAuthorization() { }

    [RequireClaim("System")]
    public void RequireSystemClaim() { }

    [RequireClaim("Admin")]
    public void RequireAdminClaim() { }

    [RequireNoClaim]
    public void RequireNoClaim() { }
}