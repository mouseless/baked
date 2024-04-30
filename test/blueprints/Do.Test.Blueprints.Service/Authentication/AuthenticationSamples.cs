using System.Security.Claims;

namespace Do.Test.Authentication;

public class AuthenticationSamples(Func<ClaimsPrincipal> _getClaims)
{
    public string? Authenticate() =>
        _getClaims().Identity?.AuthenticationType;

    public object FormPostAuthenticate(object value) =>
        value;
}