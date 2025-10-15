using Baked.Authentication;
using Baked.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Baked.Test.Authentication;

public class AuthenticationSamples(
    Func<ClaimsPrincipal> _getClaims,
    ILogger<AuthenticationSamples> _logger,
    ITokenBuilder _tokenBuilder
)
{
    public string? Authenticate() =>
        _getClaims().Identity?.AuthenticationType;

    public string? FormPostAuthenticate(string value)
    {
        _logger.LogInformation($"Form post authenticate is called with value: '{value}'");

        return _getClaims().Identity?.AuthenticationType;
    }

    [AllowAnonymous]
    public Token Login(string username)
    {
        if (string.IsNullOrEmpty(username)) { throw new InvalidOperationException("No user found with given credentials!"); }

        return CreateToken(["Admin", "User", "BaseA", "BaseB"]);
    }

    [RequireUser(["Refresh"], Override = true)]
    public Token Refresh() =>
        CreateToken(["Admin", "User", "BaseA", "BaseB"]);

    Token CreateToken(List<string> claims) =>
        new(
            _tokenBuilder.Build("access", [.. claims.Select(c => new Claim(c, c))]),
            _tokenBuilder.Build("refresh", [new("Refresh", "Refresh")])
        );
}