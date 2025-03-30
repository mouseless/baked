using Baked.Authentication;
using Baked.Authentication.Jwt;
using Baked.Testing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shouldly;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Baked;

public static class JwtAuthenticationExtensions
{
    public static JwtAuthenticationFeature Jwt(this AuthenticationConfigurator _,
        Action<JwtBearerOptions>? configureOptions = default,
        Action<JwtAuthenticationPlugin>? configurePlugin = default
    )
    {
        configureOptions ??= new(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.FromSeconds(Settings.Required<int>("Authentication:Jwt:ClockSkewInSeconds")),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Settings.Required<string>("Authentication:Jwt:Issuer"),
                ValidAudience = Settings.Required<string>("Authentication:Jwt:Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Required<string>("Authentication:Jwt:Key")))
            };
        });
        configurePlugin ??= new(_ => { });

        return new(configureOptions, configurePlugin);
    }

    public static bool IsJwt(this string token) =>
       new JwtSecurityTokenHandler().CanReadToken(token.Replace("Bearer", string.Empty).Trim());

    public static void ShouldBeJwt(this string token) =>
       IsJwt(token).ShouldBeTrue();

    public static void TheJwtSettings(this Mocker mockMe,
        string? key = null,
        string? issuer = null,
        string? audience = null,
        int? expirationForAccess = null,
        int? expirationForRefresh = null
    )
    {
        key ??= "9a0dc4b934ca4bb5b79bf43b5dcddcce";
        issuer ??= "Issuer";
        audience ??= "Audience";
        expirationForAccess ??= 5;
        expirationForRefresh ??= 50;

        mockMe.ASetting(key: "Authentication:Jwt:Key", value: key);
        mockMe.ASetting(key: "Authentication:Jwt:Issuer", value: issuer);
        mockMe.ASetting(key: "Authentication:Jwt:Audience", value: audience);
        mockMe.ASetting(key: "Authentication:Jwt:ExpiresInMinutes:Access", value: expirationForAccess.GetValueOrDefault());
        mockMe.ASetting(key: "Authentication:Jwt:ExpiresInMinutes:Refresh", value: expirationForRefresh.GetValueOrDefault());
    }

    public static JwtSecurityToken TheSecurityToken(this Stubber _, string credential) =>
        new JwtSecurityTokenHandler().ReadJwtToken(credential);

    public static JwtTokenBuilder AJwtTokenBuilder(this Stubber giveMe) =>
        new(giveMe.The<IConfiguration>(), giveMe.The<TimeProvider>());
}