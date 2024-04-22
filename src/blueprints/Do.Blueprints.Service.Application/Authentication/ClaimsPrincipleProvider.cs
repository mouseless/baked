using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Do.Authentication;

public class ClaimsPrincipleProvider<THandler>(ClaimsPrincipleProviderOptions _options)
    where THandler : IAuthenticationHandler
{
    internal ClaimsPrincipal Create(HttpRequest request, AuthenticationProperties? properties)
    {
        var identites = new List<ClaimsIdentity>();
        foreach (var item in _options.IdentityOptions)
        {
            var claims = new List<Claim>();
            foreach (var claimProvider in item.Value)
            {
                var claim = claimProvider.Create(request, properties);
                if (claim is not null)
                {
                    claims.Add(claim);
                }
            }

            identites.Add(new ClaimsIdentity(claims, item.Key));
        }

        return new ClaimsPrincipal(identites);
    }
}