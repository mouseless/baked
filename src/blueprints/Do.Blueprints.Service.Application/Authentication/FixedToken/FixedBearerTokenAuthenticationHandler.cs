using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;

namespace Do.Authentication.FixedToken;

public class FixedBearerTokenAuthenticationHandler(
    ClaimsPrincipleProvider<FixedBearerTokenAuthenticationHandler> _claimsPrincipleProvider,
    FixedBearerTokenOptions _options,
    IConfiguration _configuration,
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder
) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    string? GetToken(string tokenName) =>
        _configuration.GetValue<string>($"Authentication:FixedToken:{tokenName}");

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string? token = default;
        if (Context.Request.Headers.Authorization.Any())
        {
            var givenToken = $"{Context.Request.Headers.Authorization}".Replace("Bearer", string.Empty).Trim();
            if (!AnyTokenValidates(token => givenToken == token, out token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Attempted to perform an unauthorized operation."));
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
                return Task.FromResult(AuthenticateResult.Fail("Attempted to perform an unauthorized operation."));
            }
        }

        if (token != null)
        {
            var properties = new AuthenticationProperties();
            properties.Items.Add(TokenClaimProvider.TOKEN_KEY, token);

            var principle = _claimsPrincipleProvider.Create(Context.Request, properties);

            return Task.FromResult(AuthenticateResult.Success(new(principle, properties, Scheme.Name)));
        }

        return Task.FromResult(AuthenticateResult.NoResult());
    }

    bool AnyTokenValidates(Func<string, bool> test, [NotNullWhen(true)] out string? validToken)
    {
        validToken = default;

        foreach (var tokenName in _options.TokenNames)
        {
            var token = GetToken(tokenName);
            if (token is null) { continue; }

            if (test(token))
            {
                validToken = token;

                return true;
            }
        }

        return false;
    }
}