using Baked.Architecture;
using Baked.Authentication.FixedBearerToken;
using Baked.Testing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using System.Text.Encodings.Web;

namespace Baked;

public static class FixedBearerTokenAuthenticationFeature
{
    public static IFeature<AuthenticationFeature.Configurator> FixedBearerToken(this AuthenticationFeature.Configurator x,
        Action<List<Token>>? configure = default,
        IEnumerable<string>? formPostParameters = default,
        IEnumerable<string>? documentNames = default,
        string description = "Enter your token provided by the development team"
    )
    {
        configure ??= tokens => tokens.Add("Default");
        formPostParameters ??= [];
        documentNames ??= [string.Empty];

        var tokens = new List<Token>();
        configure(tokens);

        return x.Define(configurator =>
        {
            configurator.HttpServer.ConfigureAuthenticationCollection(authentications =>
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

            configurator.Runtime.ConfigureServiceCollection(services =>
            {
                services.AddSingleton(new TokenOptions(tokens));
            });

            configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
            {
                foreach (var documentName in documentNames)
                {
                    swaggerGenOptions.AddSecurityDefinition(nameof(FixedBearerToken),
                        new()
                        {
                            Type = SecuritySchemeType.Http,
                            Scheme = "Bearer",
                            Description = description
                        },
                        documentName: documentName
                    );

                    swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>([nameof(FixedBearerToken)],
                        documentName: documentName
                    );

                    foreach (var name in formPostParameters)
                    {
                        swaggerGenOptions.AddFormParameterToRedirectOperationsThatUse<AuthorizeAttribute>(
                            name,
                            new()
                            {
                                Type = JsonSchemaType.String,
                                Description = Settings.Optional($"Authentication:FixedBearerToken:Description:{name}", name)
                            },
                            documentName: documentName
                        );
                    }

                    swaggerGenOptions.AddFormParameterToRedirectOperationsThatUse<AuthorizeAttribute>(
                        "hash",
                        new()
                        {
                            Type = JsonSchemaType.String,
                            Description = Settings.Optional(
                                "Authentication:FixedBearerToken:Description:hash",
                                "Concatenate all form post parameters with secret " +
                                "token at the end, then hash it using `sha256` and " +
                                "convert it to `base64`"
                            )
                        },
                        documentName: documentName
                    );
                }
            });
        });
    }

    extension(List<Token> tokens)
    {
        public void Add(string name,
            IEnumerable<string>? claims = default
        ) => tokens.Add(new(name, claims ?? []));
    }

    extension(Stubber giveMe)
    {
        public IAuthenticationHandler AFixedBearerTokenAuthenticationHandler(HttpRequest request, Action<List<Token>> configure)
        {
            var tokens = new List<Token>();
            configure(tokens);

            var handler = new AuthenticationHandler(
                new(tokens),
                giveMe.The<IConfiguration>(),
                giveMe.The<IOptionsMonitor<AuthenticationSchemeOptions>>(),
                giveMe.The<ILoggerFactory>(),
                UrlEncoder.Default
            );
            handler.InitializeAsync(new AuthenticationScheme(nameof(FixedBearerToken), nameof(FixedBearerToken), handler.GetType()), request.HttpContext).GetAwaiter().GetResult();

            return handler;
        }
    }
}