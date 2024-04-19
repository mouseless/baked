using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Do.Authentication.FixedToken;

public class FixedBearerTokenAuthenticationHandler(
    IConfiguration _configuration,
    IOptionsMonitor<FixedBearerTokenOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder
) : AuthenticationHandler<FixedBearerTokenOptions>(options, logger, encoder)
{
    string? GetToken(string tokenName) =>
        _configuration.GetValue<string>($"Authentication:FixedToken:{tokenName}");

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Context.Request.Headers.Authorization.Any())
        {
            var givenToken = $"{Context.Request.Headers.Authorization}".Replace("Bearer", string.Empty).Trim();
            if (AnyTokenValidates(token => givenToken == token))
            {
                var claims = new[] { new Claim("AdminToken", "Test") };
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "AdminOnly"));
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            throw new UnauthorizedAccessException("Attempted to perform an unauthorized operation.");
        }

        if (Context.Request.HasFormContentType && Context.Request.Form.ContainsKey("hash"))
        {
            var data = Context.Request.Form.ToDictionary(kvp => kvp.Key, kvp => $"{kvp.Value}");

            var hash = string.Empty;
            var parameters = string.Empty;
            foreach (var (key, value) in data)
            {
                if (key == "hash")
                {
                    hash = $"{value}";

                    continue;
                }

                parameters += $"{value}";
            }

            if (AnyTokenValidates(token => hash == (parameters + token).ToSHA256().ToBase64()))
            {
                var claims = new[] { new Claim("AdminToken", "Test") };
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "AdminOnly"));
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            throw new UnauthorizedAccessException("Attempted to perform an unauthorized operation.");
        }

        return Task.FromResult(AuthenticateResult.NoResult());
    }

    bool AnyTokenValidates(Func<string, bool> test)
    {
        foreach (var tokenName in Options.TokenNames)
        {
            var token = GetToken(tokenName);
            if (token is null) { continue; }

            if (test(token)) { return true; }
        }

        return false;
    }
}