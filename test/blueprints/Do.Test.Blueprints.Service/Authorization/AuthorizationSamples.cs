using Microsoft.AspNetCore.Authorization;

namespace Do.Test.Authorization;

public class AuthorizationSamples
{
    [Authorize]
    public void RequireAuthorization() { }

    [Authorize(Policy = "Default")]
    public void RequireDefaultPolicy() { }

    [Authorize(Policy = "AdminOnly")]
    public void RequireAdminPolicy() { }
}