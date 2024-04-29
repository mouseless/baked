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
        Action<List<Token>>? configure = default
    )
    {
        configure ??= tokens => tokens.Add("Default");

        var tokens = new List<Token>();
        configure(tokens);

        return new(tokens);
    }

    public static void Add(this List<Token> tokens, string name,
        IEnumerable<string>? claims = default
    ) => tokens.Add(new(name, claims ?? ["User"]));

    public static IAuthenticationHandler AFixedBearerTokenAuthenticationHandler(this Stubber giveMe, HttpRequest request, Action<List<Token>> configure)
    {
        var tokens = new List<Token>();
        configure(tokens);

        var handler = new AuthenticationHandler(
            new(tokens),
            giveMe.The<IConfiguration>(),
            giveMe.The<IOptionsMonitor<AuthenticationSchemeOptions>>(),
            giveMe.The<ILoggerFactory>(),
            UrlEncoder.Default
        );
        handler.InitializeAsync(new AuthenticationScheme("FixedBearerToken", "FixedBearerToken", handler.GetType()), request.HttpContext).GetAwaiter().GetResult();

        return handler;
    }
}