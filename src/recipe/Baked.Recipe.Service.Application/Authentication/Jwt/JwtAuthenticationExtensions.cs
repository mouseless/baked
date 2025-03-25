using Baked.Authentication;
using Baked.Authentication.Jwt;
using Baked.Testing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shouldly;
using System.IdentityModel.Tokens.Jwt;

namespace Baked;

public static class JwtAuthenticationExtensions
{
    public static JwtAuthenticationFeature Jwt(this AuthenticationConfigurator _,
        Action<JwtBearerOptions>? configureOptions = default
    ) => new(configureOptions ?? (_ => { }));

    public static void ShouldBeJwt(this string token) =>
       new JwtSecurityTokenHandler().CanReadToken(token).ShouldBeTrue();

    public static void TheJwtSettings(this Mocker mockMe,
        string? key = null,
        string? issuer = null,
        string? audience = null,
        int? expirationForAccess = null,
        int? expirationForRefresh = null
    )
    {
        key ??= "f7fee531f838473b83f1f73a808acfb1";
        issuer ??= "YK";
        audience ??= "UI";
        expirationForAccess ??= 5;
        expirationForRefresh ??= 50;

        mockMe.ASetting(key: "Authentication:Jwt:Key", value: key);
        mockMe.ASetting(key: "Authentication:Jwt:Issuer", value: issuer);
        mockMe.ASetting(key: "Authentication:Jwt:Audience", value: audience);
        mockMe.ASetting(key: "Authentication:Jwt:ExpiresInMinutes:Access", value: expirationForAccess);
        mockMe.ASetting(key: "Authentication:Jwt:ExpiresInMinutes:Refresh", value: expirationForRefresh);
    }

    public static JwtSecurityToken TheSecurityToken(this Stubber _, string credential) =>
        new JwtSecurityTokenHandler().ReadJwtToken(credential);
}