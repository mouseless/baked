using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Do.Authentication;

public interface IClaimProvider
{
    Claim? Create(HttpRequest request,
        AuthenticationProperties? properties = default
    );
}