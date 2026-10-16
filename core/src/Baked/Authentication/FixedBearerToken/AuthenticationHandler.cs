using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Encodings.Web;

namespace Baked.Authentication.FixedBearerToken;

public class AuthenticationHandler(
    TokenOptions _options,
    IConfiguration _configuration,
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder
) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    const string BEARER_PREFIX = "Bearer ";
    const string HASH_KEY = "hash";

    // NOTE hashing both sides first makes comparison take the same time
    // regardless of where values differ or how long they are
    static bool FixedTimeEquals(string left, string right) =>
        CryptographicOperations.FixedTimeEquals(left.ToSHA256(), right.ToSHA256());

    // NOTE fields are url encoded `key=value` pairs sorted by key and joined
    // with `&`, so that a value cannot be shifted to another field
    static string Sign(IFormCollection form, string token)
    {
        var message = string.Join("&", form
            .Where(field => field.Key != HASH_KEY)
            .OrderBy(field => field.Key, StringComparer.Ordinal)
            .SelectMany(field => field.Value.Select(value => $"{Uri.EscapeDataString(field.Key)}={Uri.EscapeDataString(value ?? string.Empty)}"))
        );

        return HMACSHA256.HashData(token.ToUtf8Bytes(), message.ToUtf8Bytes()).ToBase64();
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Token? token = default;

        if (Context.Request.Headers.Authorization.Any())
        {
            var givenToken = $"{Context.Request.Headers.Authorization}".Trim();
            if (givenToken.StartsWith(BEARER_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                givenToken = givenToken[BEARER_PREFIX.Length..].Trim();
            }

            if (!AnyTokenValidates(token => FixedTimeEquals(givenToken, token), out token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));
            }
        }

        if (Context.Request.HasFormContentType && Context.Request.Form.ContainsKey(HASH_KEY))
        {
            var form = Context.Request.Form;
            var hash = $"{form[HASH_KEY]}";

            if (!AnyTokenValidates(token => FixedTimeEquals(hash, Sign(form, token)), out token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));
            }
        }

        if (token != null)
        {
            var identity = new ClaimsIdentity("FixedBearerToken");
            identity.AddClaim(new(ClaimTypes.Name, token.Name));
            identity.AddClaims(token.Claims.Select(c => new Claim(c, c)));
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
            if (string.IsNullOrEmpty(value)) { continue; }

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