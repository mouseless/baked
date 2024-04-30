using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Do.Authentication.FixedBearerToken;

public class AuthenticationHandler(
    TokenOptions _options,
    IConfiguration _configuration,
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder
) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Token? token = default;

        if (Context.Request.Headers.Authorization.Any())
        {
            var givenToken = $"{Context.Request.Headers.Authorization}".Replace("Bearer", string.Empty).Trim();
            if (!AnyTokenValidates(token => givenToken == token, out token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));
            }
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

            if (!AnyTokenValidates(token => hash == (parameters + token).ToSHA256().ToBase64(), out token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));
            }
        }

        if (token != null)
        {
            var identity = new ClaimsIdentity(token.Claims.Select(c => new Claim(c, c)), "FixedBearerToken");
            var principal = new ClaimsPrincipal(identity);

            return Task.FromResult(AuthenticateResult.Success(new(principal, Scheme.Name)));
        }

        return Task.FromResult(AuthenticateResult.NoResult());
    }

    bool AnyTokenValidates(Func<string, bool> test, [NotNullWhen(true)] out Token? validToken)
    {
        foreach (var token in _options.Tokens)
        {
            var value = _configuration.GetValue<string>($"Authentication:FixedBearerToken:{token.Name}");
            if (value is null) { continue; }

            if (test(value))
            {
                validToken = token;

                return true;
            }
        }

        validToken = default;

        return false;
    }
}