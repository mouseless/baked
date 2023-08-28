using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.ExceptionHandling.Default;

public class DefaultExceptionHandlingFeature : IFeature<ExceptionHandlingConfigurator>
{
    readonly List<Type> _handlers = new();
    readonly IExceptionHandler _defaultHandler = new DefaultExceptionHandler();

    public DefaultExceptionHandlingFeature(List<Type> handlers)
    {
        _handlers.AddRange(handlers);
    }

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            foreach (var item in _handlers)
            {
                services.AddTransient(typeof(IExceptionHandler), item);
            }
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<ExceptionHandlingMiddleware>(order: -20);
        });
    }
}
