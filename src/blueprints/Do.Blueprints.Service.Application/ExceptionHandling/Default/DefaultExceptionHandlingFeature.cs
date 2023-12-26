using Do.Architecture;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Do.ExceptionHandling.Default;

public class DefaultExceptionHandlingFeature : IFeature<ExceptionHandlingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<IExceptionHandler, HandledExceptionHandler>();

            services.AddExceptionHandler<ExceptionHandler>();
            services.AddProblemDetails();
        });

        configurator.ConfigureMiddlewareCollection(middleware =>
        {
            middleware.Add(app => app.UseExceptionHandler());
        });
    }
}
