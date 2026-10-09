using Baked.Architecture;
using Baked.Authorization;
using Baked.RestApi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace Baked.Authentication.Jwt;

public class JwtAuthenticationFeature(Action<JwtBearerOptions> _configureOptions, Action<JwtAuthenticationPlugin> _configurePlugin)
    : IFeature<AuthenticationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.HttpServer.ConfigureAuthenticationCollection(authentications =>
        {
            authentications.Add(
                scheme: JwtBearerDefaults.AuthenticationScheme,
                useBuilder: builder => builder.AddJwtBearer(options => _configureOptions(options)),
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

            _configurePlugin(plugin);
            plugin.AnonymousPageRoutes.Add(plugin.LoginPageRoute);
            app.Plugins.Add(plugin);
        });
    }
}