using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Do.Authentication;

public interface IClaimsPrincipleProvider
{
    ClaimsPrincipal? Create(HttpRequest request,
        AuthenticationProperties? properties = default
    );
}