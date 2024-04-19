using Microsoft.AspNetCore.Authorization;

namespace Do.Test.Authorization;

public class AuthorizationSamples
{
    [Authorize(Policy = "AdminOnly")]
    public void ClaimBasedAuthorization() { }
}
