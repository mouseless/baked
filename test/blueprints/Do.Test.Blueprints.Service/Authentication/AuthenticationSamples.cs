using System.Security.Claims;

namespace Do.Test.Authentication;

public class AuthenticationSamples(Func<ClaimsPrincipal> _getClaims)
{
    public string? TokenAuthentication() =>
        _getClaims().Claims.FirstOrDefault()?.Value;

    public object FormPostAuthentication(object value) => value;
}