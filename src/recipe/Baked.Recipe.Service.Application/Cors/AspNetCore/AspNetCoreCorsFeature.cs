using Baked.Architecture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Cors.AllowOrigin;

public class AspNetCoreCorsFeature(Action<CorsOptions> _optionsBuilder, string _defaultPolicyName)
    : IFeature<CorsConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddCors(options => _optionsBuilder(options));
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseCors(_defaultPolicyName));
        });
    }
}