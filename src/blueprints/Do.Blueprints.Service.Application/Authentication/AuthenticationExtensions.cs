using Do.Architecture;
using Do.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Do;

public static class AuthenticationExtensions
{
    public static void AddAuthentication(this IList<IFeature> source, Func<AuthenticationConfigurator, IFeature> configure) => source.Add(configure(new()));

    public static void AddSecurityRequirementToOperationsThatUse<TMiddleware>(this SwaggerGenOptions source, string schemeId) => source.OperationFilter<SecurityRequirementOperationFilter<UseAttribute<TMiddleware>>>([schemeId]);
}
