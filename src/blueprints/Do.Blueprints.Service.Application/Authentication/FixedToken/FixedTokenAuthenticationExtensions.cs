using Do.Authentication;
using Do.Authentication.FixedToken;
using Do.Testing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Do;

public static class FixedTokenAuthenticationExtensions
{
    public static FixedTokenAuthenticationFeature FixedToken(this AuthenticationConfigurator _,
        string[]? tokenNames = default,
        Action<ClaimsPrincipalProviderOptions>? _optionsBuilder = default
    ) => new([.. (tokenNames ?? ["Default"])], _optionsBuilder ?? (_ => { }));

    public static IAuthenticationHandler AFixedBearerTokenAuthenticationHandler(this Stubber giveMe, HttpRequest request,
       string[]? tokenNames = default
    )
    {
        tokenNames ??= [];

        var options = giveMe.The<FixedBearerTokenOptions>();
        options.TokenNames.AddRange(tokenNames);

        var handler = giveMe.A<FixedBearerTokenAuthenticationHandler>();

        handler.InitializeAsync(new AuthenticationScheme("FixedBearerToken", "FixedBearerToken", handler.GetType())!, request.HttpContext).GetAwaiter().GetResult();

        return handler;
    }
}