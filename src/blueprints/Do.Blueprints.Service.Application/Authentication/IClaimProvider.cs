using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Do.Authentication;

public interface IClaimProvider
{
    Claim? Create(HttpContext context,
        AuthenticationProperties? properties = default
    );
}