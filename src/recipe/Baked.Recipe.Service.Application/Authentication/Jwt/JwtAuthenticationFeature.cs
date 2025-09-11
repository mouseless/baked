using Baked.Architecture;
using Baked.RestApi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Baked.Authentication.Jwt;

public class JwtAuthenticationFeature(Action<JwtBearerOptions> _configureOptions, Action<JwtAuthenticationPlugin> _configurePlugin)
    : IFeature<AuthenticationConfigurator>
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
            var plugin = new JwtAuthenticationPlugin();

            configurator.UsingDomainModel(domain =>
            {
                plugin.AnonymousApiRoutes.AddRange(
                    domain.Types
                        .Having<ControllerModelAttribute>()
                        .SelectMany(t => t.GetMembers().Methods.Having<ActionModelAttribute>())
                        .Where(m => m.Has<Authorization.AllowAnonymousAttribute>())
                        .Select(m => m.Get<ActionModelAttribute>().GetRoute())
                    );
            });

            _configurePlugin(plugin);
            plugin.AnonymousPageRoutes.Add(plugin.LoginPageRoute);
            app.Plugins.Add(plugin);
        });
    }
}