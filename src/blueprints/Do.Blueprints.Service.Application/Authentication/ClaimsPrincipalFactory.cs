using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Do.Authentication;

public class ClaimsPrincipalFactory(ClaimsPrincipalFactoryOptions _options)
{
    internal ClaimsPrincipal Create(HttpContext context, AuthenticationProperties? properties)
    {
        var identites = new List<ClaimsIdentity>();
        foreach (var item in _options)
        {
            var claims = new List<Claim>();
            foreach (var claimProvider in item.Value)
            {
                var claim = claimProvider.Create(context, properties);
                if (claim is not null)
                {
                    claims.Add(claim);
                }
            }

            if (claims.Any())
            {
                identites.Add(new ClaimsIdentity(claims, item.Key));
            }
        }

        return new(identites);
    }
}