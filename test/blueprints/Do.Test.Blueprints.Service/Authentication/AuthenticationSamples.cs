using System.Security.Claims;

namespace Do.Test.Authentication;

public class AuthenticationSamples(Func<ClaimsPrincipal> _getClaims)
{
    public string? Authenticate() =>
        _getClaims().Identity?.AuthenticationType;

    public string? FormPostAuthenticate(object value) =>
        _getClaims().Identity?.AuthenticationType;
}