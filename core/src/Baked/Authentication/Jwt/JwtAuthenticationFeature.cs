using Baked.Architecture;
using Baked.Authentication;
using Baked.Authentication.Jwt;
using Baked.Authorization;
using Baked.RestApi.Model;
using Baked.Testing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Shouldly;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Baked;

public static class JwtAuthenticationFeature
{
    public static IFeature<AuthenticationFeature.Configurator> Jwt(this AuthenticationFeature.Configurator x,
        Action<JwtBearerOptions>? configureOptions = default,
        Action<JwtAuthenticationPlugin>? configurePlugin = default
    ) => x.Define(configurator =>
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

        configurator.HttpServer.ConfigureAuthenticationCollection(authentications =>
        {
            authentications.Add(
                scheme: JwtBearerDefaults.AuthenticationScheme,
                useBuilder: builder => builder.AddJwtBearer(options => configureOptions(options)),
                handles: context => context.Request.Headers.Authorization.Any(h => h is not null && h.Contains("Bearer") && h.IsJwt())
            );
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITokenBuilder, JwtTokenBuilder>();
        });

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                }
            );

            swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>([JwtBearerDefaults.AuthenticationScheme]);
        });

        configurator.Ui.ConfigureAppDescriptor(app =>
        {
            var plugin = new JwtAuthenticationPlugin();

            configurator.Domain.UsingDomainModel(domain =>
            {
                plugin.AnonymousApiRoutes.AddRange(
                    domain.Types
                        .Having<ApiController>()
                        .SelectMany(t => t.GetMembers().Methods.Having<ApiAction>())
                        .Where(m => m.Has<AllowAnonymous>())
                        .Select(m => new AnonymousApiRoute(m.Get<ApiAction>().Method.Method, m.Get<ApiAction>().GetRoute()))
                    );
            });

            configurePlugin(plugin);
            plugin.AnonymousPageRoutes.Add(plugin.LoginPageRoute);
            app.Plugins.Add(plugin);
        });
    });

    extension(string token)
    {
        public bool IsJwt() =>
            new JwtSecurityTokenHandler().CanReadToken(token.Replace("Bearer", string.Empty).Trim());

        public void ShouldBeJwt() =>
           token.IsJwt().ShouldBeTrue();
    }

    extension(Mocker mockMe)
    {
        public void TheJwtSettings(
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
    }

    extension(Stubber giveMe)
    {
        public JwtSecurityToken TheSecurityToken(string credential) =>
            new JwtSecurityTokenHandler().ReadJwtToken(credential);

        public JwtTokenBuilder AJwtTokenBuilder() =>
            new(giveMe.The<IConfiguration>(), giveMe.The<TimeProvider>());
    }
}