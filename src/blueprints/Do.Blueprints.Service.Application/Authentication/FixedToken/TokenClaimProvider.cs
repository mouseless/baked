using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Do.Authentication.FixedToken;

public class TokenClaimProvider : IClaimsPrincipleProvider
{
    public ClaimsPrincipal? Create(HttpRequest request, AuthenticationProperties? properties)
    {
        if (properties?.Items.TryGetValue("Token", out string? propertyToken) == true)
        {
            return new(new ClaimsIdentity([new("Token", $"{propertyToken}")], "Admin"));
        }

        var headerToken = $"{request.Headers.Authorization}".Replace("Bearer", string.Empty).Trim();
        if (!string.IsNullOrEmpty(headerToken))
        {
            return new(new ClaimsIdentity([new("Token", $"{headerToken}")], "Admin"));
        }

        return default;
    }
}