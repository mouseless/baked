using Do.Authentication;
using Do.Authentication.FixedBearerToken;
using Do.Testing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace Do;

public static class FixedBearerTokenAuthenticationExtensions
{
    public static FixedBearerTokenAuthenticationFeature FixedBearerToken(this AuthenticationConfigurator _,
        Action<FixedBearerTokenOptions>? configure = default
    )
    {
        configure ??= _ => { };

        var options = new FixedBearerTokenOptions();
        configure(options);

        return new(options);
    }

    public static IAuthenticationHandler AFixedBearerTokenAuthenticationHandler(this Stubber giveMe, HttpRequest request, Action<FixedBearerTokenOptions> tokens)
    {
        var options = new FixedBearerTokenOptions();
        tokens(options);

        var handler = new FixedBearerTokenAuthenticationHandler(
            options,
            giveMe.The<IConfiguration>(),
            giveMe.The<IOptionsMonitor<AuthenticationSchemeOptions>>(),
            giveMe.The<ILoggerFactory>(),
            UrlEncoder.Default
        );
        handler.InitializeAsync(new AuthenticationScheme("FixedBearerToken", "FixedBearerToken", handler.GetType()), request.HttpContext).GetAwaiter().GetResult();

        return handler;
    }
}