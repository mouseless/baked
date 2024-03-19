using Do.Architecture;
using Do.Authentication;
using Do.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Do;

public static class AuthenticationExtensions
{
    public static void AddAuthentication(this IList<IFeature> source, Func<AuthenticationConfigurator, IFeature> configure) => source.Add(configure(new()));

    public static void AddSecurityRequirementToOperationsThatUse<TMiddleware>(this SwaggerGenOptions source, string schemeId) => source.OperationFilter<SecurityRequirementOperationFilter<UseAttribute<TMiddleware>>>([schemeId]);
    public static void AddHeaderToOperationsThatUse<TMiddleware>(this SwaggerGenOptions source, params string[] headers) => source.OperationFilter<AddHeaderOperationFilter<UseAttribute<TMiddleware>>>([headers]);

    public static Do.Authentication.FixedToken.Middleware AFixedTokenMiddleware(this Stubber giveMe,
       string[]? tokenNames = default
    )
    {
        tokenNames ??= [];

        return new(_ => Task.CompletedTask, giveMe.The<IConfiguration>(), new(TokenNames: [.. tokenNames]));
    }
}
