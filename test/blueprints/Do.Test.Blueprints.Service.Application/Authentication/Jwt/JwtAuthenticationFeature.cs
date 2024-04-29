using Do.Architecture;
using Do.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Do.Test.Authentication.Jwt;

public class JwtAuthenticationFeature : IFeature<AuthenticationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAuthentication(configuration =>
        {
            configuration.Add(
                    JwtBearerDefaults.AuthenticationScheme,
                    context =>
                    {
                        if (context.Request.Headers.TryGetValue("Authorization", out var value) && value.ToString().Contains("Bearer"))
                        {
                            if (new JwtSecurityTokenHandler().CanReadToken(value.ToString().Replace("Bearer", string.Empty).Trim()))
                            {
                                return true;
                            }

                            return false;
                        }

                        return false;
                    },
                    useBuilder: builder => builder.AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            RequireAudience = false,
                            RequireExpirationTime = false,
                            RequireSignedTokens = false,
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = false,
                            ValidateIssuerSigningKey = false
                        };
                    })
                );
        });
    }
}