using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Do.Authentication.FixedToken;

public class TokenClaimProvider : IClaimProvider
{
    public const string TOKEN_KEY = "Token";

    public Claim? Create(HttpContext context,
        AuthenticationProperties? properties = null
    )
    {
        if (properties is not null && properties.Items.TryGetValue(TOKEN_KEY, out string? propertyToken))
        {
            return new(TOKEN_KEY, $"{propertyToken}");
        }

        var headerToken = $"{context.Request.Headers.Authorization}".Replace("Bearer", string.Empty).Trim();
        if (!string.IsNullOrEmpty(headerToken))
        {
            return new(TOKEN_KEY, $"{headerToken}");
        }

        return default;
    }
}