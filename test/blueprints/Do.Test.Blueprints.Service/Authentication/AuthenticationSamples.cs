using System.Security.Claims;

namespace Do.Test.Authentication;

public class AuthenticationSamples(Func<ClaimsPrincipal> _getClaims)
{
    public string? GetAuthenticationType() =>
        _getClaims().Identity?.AuthenticationType;

    public object FormPostAuthentication(object value) =>
        value;
}