using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.ExceptionHandling.Default;

public class DefaultExceptionHandlingFeature : IFeature<ExceptionHandlingConfigurator>
{
    public DefaultExceptionHandlingFeature()
    {
    }

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddTransient<IExceptionHandler, HandledExceptionHandler>();
            services.AddTransient<IExceptionHandler, DefaultExceptionHandler>();
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<ExceptionHandlingMiddleware>(order: -20);
        });
    }
}
