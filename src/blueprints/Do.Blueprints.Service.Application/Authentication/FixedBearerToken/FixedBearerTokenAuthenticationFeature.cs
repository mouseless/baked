using Do;
using Do.Architecture;
using Humanizer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Do.Authentication.FixedBearerToken;

public class FixedBearerTokenAuthenticationFeature(List<Token> _tokens, List<string> formPostParameters)
    : IFeature<AuthenticationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAuthenticationCollection(authentications =>
        {
            authentications.Add(
                scheme: "FixedBearerToken",
                useBuilder: builder => builder
                    .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>(
                        "FixedBearerToken",
                        opts => { }
                    ),
                handles: context =>
                    context.Request.Headers.Authorization.Any() ||
                    (context.Request.HasFormContentType && context.Request.Form.ContainsKey("hash"))
            );
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(new TokenOptions(_tokens));
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.AddSecurityDefinition("FixedBearerToken",
                new()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = $"Enter token for {_tokens.Select(t => t.Name).Humanize("or")}",
                }
            );

            swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>("FixedBearerToken");
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            var domainModel = configurator.Context.GetDomainModel();
            foreach (var name in formPostParameters)
            {
                conventions.Add(new AddParameterToFormPostConvention(domainModel, name), order: int.MaxValue);
            }

            conventions.Add(new AddParameterToFormPostConvention(domainModel, "hash"), order: int.MaxValue);
        });
    }
}