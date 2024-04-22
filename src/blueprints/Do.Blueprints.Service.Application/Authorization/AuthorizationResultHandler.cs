using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Authentication;

namespace Do.Authorization;

public class AuthorizationResultHandler : IAuthorizationMiddlewareResultHandler
{
    readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (!authorizeResult.Succeeded)
        {
            if (authorizeResult.Challenged)
            {
                throw new AuthenticationException(@"Attempted to perform an unauthorized operation.");
            }

            if (authorizeResult.Forbidden)
            {
                throw new UnauthorizedAccessException(@"Attempted to perform an unauthorized operation.");
            }
        }

        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}