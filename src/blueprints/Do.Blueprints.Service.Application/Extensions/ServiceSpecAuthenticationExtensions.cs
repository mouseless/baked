using Do.Testing;
using Microsoft.Extensions.Configuration;

namespace Do;

public static class ServiceSpecAuthenticationExtensions
{
    public static Do.Authentication.FixedToken.Middleware AFixedTokenMiddleware(this Stubber giveMe,
       string[]? tokenNames = default
    )
    {
        tokenNames ??= [];

        return new(_ => Task.CompletedTask, giveMe.The<IConfiguration>(), new(TokenNames: [.. tokenNames]));
    }
}
