using Do.Authorization;

namespace Do.Test.Authorization;

public class AuthorizationSamples
{
    public void RequireAuthorization() { }

    [RequireClaim("System")]
    public void RequireDefaultPolicy() { }

    [RequireClaim("Admin")]
    public void RequireAdminPolicy() { }
}