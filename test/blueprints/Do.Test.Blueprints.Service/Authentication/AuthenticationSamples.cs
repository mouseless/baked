using Do.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Do.Test.Authentication;

public class AuthenticationSamples(Func<ClaimsPrincipal> _getClaims)
{
    public object? GetClaims() =>
        ToIdentityList(_getClaims());

    public object FormPostAuthentication(object value) =>
        value;

    [RequireNoClaim]
    public string CreateJwtToken()
    {
        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(claims: [new("User", "User")], expires: DateTime.Now.AddMinutes(120)));
    }

    IEnumerable<dynamic> ToIdentityList(ClaimsPrincipal source)
    {
        foreach (var identity in source.Identities)
        {
            yield return new
            {
                Name = identity.AuthenticationType ?? "Anonymous",
                Claims = identity.Claims.Select(c => new { c.Type, c.Value })
            };
        }
    }
}