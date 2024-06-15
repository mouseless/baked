using Baked.Authentication;
using Baked.Authentication.FixedBearerToken;
using Baked.Testing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace Baked;

public static class FixedBearerTokenAuthenticationExtensions
{
    public static FixedBearerTokenAuthenticationFeature FixedBearerToken(this AuthenticationConfigurator _,
        Action<List<Token>>? configure = default,
        IEnumerable<string>? formPostParameters = default,
        IEnumerable<string>? documentNames = default,
        string description = "Enter your token provided by the development team"
    )
    {
        configure ??= tokens => tokens.Add("Default");
        formPostParameters ??= [];
        documentNames ??= [string.Empty];

        var tokens = new List<Token>();
        configure(tokens);

        return new(tokens, formPostParameters, documentNames, description);
    }

    public static void Add(this List<Token> tokens, string name,
        IEnumerable<string>? claims = default
    ) => tokens.Add(new(name, claims ?? []));

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
        handler.InitializeAsync(new AuthenticationScheme(nameof(FixedBearerToken), nameof(FixedBearerToken), handler.GetType()), request.HttpContext).GetAwaiter().GetResult();

        return handler;
    }
}