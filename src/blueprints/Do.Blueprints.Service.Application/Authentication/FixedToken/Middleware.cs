using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Do.Authentication.FixedToken;

public class Middleware(
    RequestDelegate _next,
    IConfiguration _configuration,
    Options _options
)
{
    string? GetToken(string tokenName) =>
        _configuration.GetValue<string>($"Authentication:FixedToken:{tokenName}");

    public async Task Invoke(HttpContext context)
    {
        if (!context.HasMetadata<UseAttribute<Middleware>>())
        {
            await _next(context);

            return;
        }

        if (context.Request.Headers.Authorization.Any())
        {
            var givenToken = $"{context.Request.Headers.Authorization}".Replace("Bearer", string.Empty).Trim();
            if (AnyTokenValidates(token => givenToken == token))
            {
                await _next(context);

                return;
            }
        }

        if (context.Request.HasFormContentType && context.Request.Form.ContainsKey("hash"))
        {
            var data = context.Request.Form.ToDictionary(kvp => kvp.Key, kvp => $"{kvp.Value}");

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
                await _next(context);

                return;
            }
        }

        throw new UnauthorizedAccessException();
    }

    bool AnyTokenValidates(Func<string, bool> test)
    {
        foreach (var tokenName in _options.TokenNames)
        {
            var token = GetToken(tokenName);
            if (token is null) { continue; }

            if (test(token)) { return true; }
        }

        return false;
    }
}
