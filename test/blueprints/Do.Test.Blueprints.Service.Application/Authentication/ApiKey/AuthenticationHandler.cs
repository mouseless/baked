using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Do.Test.Authentication.ApiKey;

public class AuthenticationHandler(
    ApiKeyOptions _options,
    IConfiguration _configuration,
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder
) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Context.Request.Headers.TryGetValue("X-Api-Key".ToLowerInvariant(), out var value))
        {
            if (!ApiKeys.Any(a => a == value.ToString()))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));
            }

            var identity = new ClaimsIdentity(_options.Claims.Select(c => new Claim(c, c)), _options.IdentityName);
            var principal = new ClaimsPrincipal(identity);

            return Task.FromResult(AuthenticateResult.Success(new(principal, Scheme.Name)));
        }

        return Task.FromResult(AuthenticateResult.NoResult());
    }

    List<string> ApiKeys => _configuration.GetRequiredSection("Authentication:ApiKey").Get<List<string>>() ?? [];
}