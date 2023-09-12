using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.ExceptionHandling.Default;

public class DefaultExceptionHandlingFeature : IFeature<ExceptionHandlingConfigurator>
{
    readonly List<Type> _handlers = new();

    public DefaultExceptionHandlingFeature(List<Type> handlers)
    {
        _handlers.AddRange(handlers);
        _handlers.Add(typeof(HandledExceptionHandler));
        _handlers.Add(typeof(DefaultExceptionHandler));
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
