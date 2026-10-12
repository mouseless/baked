using Baked.Architecture;
using Baked.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class AspNetCoreCorsFeature
{
    /// <note>
    /// Returns 'AspNetCoreCors' feature with a single policy setup with given origins,
    /// any header and any method
    /// </note>
    public static IFeature<CorsFeature.Configurator> AspNetCore(this CorsFeature.Configurator x,
        params IEnumerable<Setting<string>> origins
    ) => x.AspNetCore(
        options => options
            .AddPolicy("allow-origin", policy => policy
                .WithOrigins([.. origins.Select(o => o.GetValue())])
                .AllowAnyHeader()
                .AllowAnyMethod()
            ),
        "allow-origin"
    );

    public static IFeature<CorsFeature.Configurator> AspNetCore(this CorsFeature.Configurator x,
        Action<CorsOptions> optionsBuilder,
        string defaultPolicyName
    ) => x.Define(configurator =>
    {
        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddCors(options => optionsBuilder(options));
        });

        configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseCors(defaultPolicyName));
        });
    });
}