using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Authentication;

namespace Baked.Authorization;

public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (!authorizeResult.Succeeded)
        {
            if (authorizeResult.Challenged)
            {
                throw new AuthenticationException("Failed_to_authenticate_with_given_credentials");
            }

            if (authorizeResult.Forbidden)
            {
                throw new UnauthorizedAccessException("Attempted_to_perform_an_unauthorized_operation");
            }
        }

        await next(context);
    }
}