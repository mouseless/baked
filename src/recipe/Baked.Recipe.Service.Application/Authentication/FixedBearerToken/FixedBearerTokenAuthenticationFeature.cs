using Baked;
using Baked.Architecture;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Baked.Authentication.FixedBearerToken;

public class FixedBearerTokenAuthenticationFeature(IEnumerable<Token> _tokens, IEnumerable<string> _formPostParameters, IEnumerable<string> _documentNames, string description)
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
            foreach (var documentName in _documentNames)
            {
                swaggerGenOptions.AddSecurityDefinition("FixedBearerToken",
                    new()
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        Description = description,
                    },
                    documentName: documentName
                );

                swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>("FixedBearerToken",
                    documentName: documentName
                );
            }
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            var domainModel = configurator.Context.GetDomainModel();
            foreach (var name in _formPostParameters)
            {
                conventions.Add(new AddParameterToFormPostConvention(domainModel, name), order: int.MaxValue);
            }

            conventions.Add(new AddParameterToFormPostConvention(domainModel, "hash"), order: int.MaxValue);
        });
    }
}