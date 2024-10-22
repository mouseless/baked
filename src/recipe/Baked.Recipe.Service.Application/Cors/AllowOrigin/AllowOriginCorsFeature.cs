using Baked.Architecture;
using Baked.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Cors.AllowOrigin;

public class AllowOriginCorsFeature(Setting<string>[] _origins)
    : IFeature<CorsConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddCors(options => options
                .AddPolicy(nameof(AllowOriginCorsFeature), policy => policy
                    .WithOrigins(_origins.Select(o => o.GetValue()).ToArray())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                )
            );
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseCors(nameof(AllowOriginCorsFeature)));
        });
    }
}