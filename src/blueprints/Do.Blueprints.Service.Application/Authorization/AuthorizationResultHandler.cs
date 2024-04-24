using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Do.Authorization;

public class AuthorizationResultHandler : IAuthorizationMiddlewareResultHandler
{
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (!authorizeResult.Succeeded)
        {
            if (authorizeResult.Challenged)
            {
                throw new UnauthorizedAccessException(@"Attempted to perform an unauthorized operation.");
            }

            if (authorizeResult.Forbidden)
            {
                throw new ForbiddenAccessException(@"Attempted to perform a forbidden operation.");
            }
        }

        await next(context);
    }
}