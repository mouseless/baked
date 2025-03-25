using Baked.Authentication;
using Baked.Authentication.Jwt;
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
}
