using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Baked.Authentication.Jwt;

public class JwtTokenBuilder(IConfiguration _configuration, TimeProvider _timeProvider)
    : ITokenBuilder
{
    readonly int _defaultExpiresInMinutes = 20;

    string Key => _configuration.GetRequiredValue<string>($"Authentication:Jwt:{nameof(Key)}");
    string? Issuer => _configuration.GetValue<string>($"Authentication:Jwt:{nameof(Issuer)}");
    string? Audience => _configuration.GetValue<string>($"Authentication:Jwt:{nameof(Audience)}");

    public string Build(string tokenType, List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var header = new JwtHeader(creds);
        var payload = new JwtPayload(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            notBefore: null,
            issuedAt: _timeProvider.GetNow(),
            expires: _timeProvider.GetNow().AddMinutes(GetExpirationInMinutes(tokenType, _defaultExpiresInMinutes))
        );

        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(header, payload));
    }

    int GetExpirationInMinutes(string tokenType, int defaultValue) =>
        _configuration.GetRequiredValue($"Authentication:Jwt:ExpiresInMinutes:{tokenType}", _configuration.GetRequiredValue($"Authentication:Jwt:ExpiresInMinutes", defaultValue));
}