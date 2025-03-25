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
        _logger.LogInformation($"Form post authenticate is called with value:'{value}'");

        return _getClaims().Identity?.AuthenticationType;
    }

    [AllowAnonymous]
    public object Login(string username, string password) =>
        !(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        ? CreateToken(["User"])
        : throw new InvalidOperationException("No user found with given credentials!");

    [RequireUser(["Refresh"], Override = true)]
    public object Refresh() =>
        CreateToken(["User"]);

    object CreateToken(List<string> claims) =>
        new
        {
            Access = _tokenBuilder.Build("access", claims.Select(c => new Claim(c, c)).ToList()),
            Refresh = _tokenBuilder.Build("refresh", [new("Refresh", "Refresh")])
        };
}