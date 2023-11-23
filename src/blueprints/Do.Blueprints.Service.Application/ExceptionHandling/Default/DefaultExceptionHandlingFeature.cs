using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.ExceptionHandling.Default;

public class DefaultExceptionHandlingFeature : IFeature<ExceptionHandlingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<IExceptionHandler, HandledExceptionHandler>();
            services.AddSingleton<IExceptionHandler, CorruptedJsonDataExceptionHandler>();
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<ExceptionHandlingMiddleware>(order: -20);
        });
    }
}
