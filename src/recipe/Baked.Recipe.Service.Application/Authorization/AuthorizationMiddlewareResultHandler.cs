using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Authentication;

namespace Do.Authorization;

public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (!authorizeResult.Succeeded)
        {
            if (authorizeResult.Challenged)
            {
                throw new AuthenticationException("Failed to authenticate with given credentials.");
            }

            if (authorizeResult.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
        }

        await next(context);
    }
}