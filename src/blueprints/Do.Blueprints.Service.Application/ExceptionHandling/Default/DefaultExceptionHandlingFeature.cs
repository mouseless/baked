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
            services.AddSingleton<IExceptionHandler, HandledExceptionHandler>();
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<ExceptionHandlingNHibernate>(order: -20);
            middlewares.Add<ExceptionHandlingMiddleware>(order: -21);
        });
    }
}
