using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Do.Test.Authentication.ApiKey;

public class AuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder
) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Context.Request.Headers["X-Api-Key"] != "apikey")
        {
            return Task.FromResult(AuthenticateResult.Fail("X-Api-Key expects \"apikey\""));
        }

        var identity = new ClaimsIdentity(
            claims: [new("BaseA", "BaseA"), new("BaseB", "BaseB")],
            authenticationType: "ApiKey"
        );
        var principal = new ClaimsPrincipal(identity);

        return Task.FromResult(AuthenticateResult.Success(new(principal, Scheme.Name)));
    }
}