using System.Security.Claims;

namespace Do.Test.Authentication;

public class AuthenticationSamples(Func<ClaimsPrincipal> _getClaims)
{
    public object? GetClaims() =>
        ToIdentityList(_getClaims());

    public object FormPostAuthentication(object value) => value;

    IEnumerable<IdentityData> ToIdentityList(ClaimsPrincipal source)
    {
        foreach (var identity in source.Identities)
        {
            yield return new(identity.AuthenticationType ?? "Anonymous", identity.Claims.Select(c => new ClaimData(c.Type, c.Value)));
        }
    }
}