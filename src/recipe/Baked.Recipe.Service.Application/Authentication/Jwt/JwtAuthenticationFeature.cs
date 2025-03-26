using Baked.Architecture;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Baked.Authentication.Jwt;

public class JwtAuthenticationFeature(Action<JwtBearerOptions> _configureOptions) : IFeature<AuthenticationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAuthenticationCollection(authentications =>
        {
            authentications.Add(
                scheme: JwtBearerDefaults.AuthenticationScheme,
                useBuilder: builder => builder.AddJwtBearer(options => _configureOptions(options)),
                handles: context => context.Request.Headers.Authorization.Any(h => h is not null && h.Contains("Bearer") && h.IsJwt())
            );
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITokenBuilder, JwtTokenBuilder>();
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                new()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                }
            );

            swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>([JwtBearerDefaults.AuthenticationScheme]);
        });

        configurator.ConfigureAppDescriptor(app =>
        {
            app.Plugins.Add(new JwtAuthenticationPlugin());
        });
    }
}