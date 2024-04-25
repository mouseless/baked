using Do.Authorization;

namespace Do.Test.Authorization;

public class AuthorizationSamples
{
    public void RequireBaseClaim() { }

    [RequireClaim("System")]
    public void RequireSystemClaim() { }

    [RequireClaim("Admin")]
    public void RequireAdminClaim() { }

    [RequireNoClaim]
    public void RequireNoClaim() { }
}