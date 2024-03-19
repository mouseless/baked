using Do.Authentication;
using Do.Authentication.FixedToken;
using Do.Testing;
using Microsoft.Extensions.Configuration;

namespace Do;

public static class FixedTokenAuthenticationExtensions
{
    public static FixedTokenAuthenticationFeature FixedToken(this AuthenticationConfigurator _,
        string[]? tokenNames = default
    ) => new([.. (tokenNames ?? ["Default"])]);

    public static Middleware AFixedTokenMiddleware(this Stubber giveMe,
       string[]? tokenNames = default
    )
    {
        tokenNames ??= [];

        return new(_ => Task.CompletedTask, giveMe.The<IConfiguration>(), new(TokenNames: [.. tokenNames]));
    }
}
